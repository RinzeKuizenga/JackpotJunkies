using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;

    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int health;
    [SerializeField] private int block = 0;
    [SerializeField] private int attackDamage = 6;
    [SerializeField] private int buff = 0;

    [SerializeField] private int attackDebuff = 0;
    
    [SerializeField] private GameObject winScreen;

    public Slider enemySlider;
    public Image sliderFill;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI statusText;

    public int Health => health;
    public int Block => block;
    public int Buff => buff;
    public int AttackDebuff => attackDebuff;

    [System.Serializable]
    private class AttackModifier {
        public int amount;
        public int turnsLeft;
    }

    private readonly List<AttackModifier> _attackMods = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        health = maxHealth;
        UpdateHealthBar();
    }

    public void ApplyAttackDebuff(int amount, int turns = 3) {
        amount = Mathf.Max(0, amount);
        turns = Mathf.Max(1, amount);
        _attackMods.Add(new AttackModifier {amount = -amount, turnsLeft = turns});
        UpdateStatusText();
    }

    public int CalculateOutgoingEnemyDamage(int baseDamage) {
        int totalMod = 0;
        foreach (AttackModifier m in _attackMods) totalMod += m.amount;
        return Mathf.Max(0, baseDamage + totalMod);
    }

    public void TickAttackModifiers() {
        for (int i = _attackMods.Count - 1; i >= 0; i--)
        {
            _attackMods[i].turnsLeft--;
            if (_attackMods[i].turnsLeft <= 0)
                _attackMods.RemoveAt(i);
        }

        UpdateStatusText();
    }

    public void TakeDamage(int amount)
    {
        int damageLeft = amount;

        if (block > 0)
        {
            int blockUsed = Mathf.Min(block, damageLeft);
            block -= blockUsed;
            damageLeft -= blockUsed;
        }

        health -= damageLeft;
        health = Mathf.Max(health, 0);

        UpdateHealthBar();

        if (health <= 0)
        {
            Die();
        }
    }

    public void AddBlock(int amount)
    {
        block += amount;
        UpdateHealthBar();
    }

    public void AddBuff(int amount, int turns = 3) {
        amount = Mathf.Max(0, amount);
        turns = Mathf.Max(1, turns);
        _attackMods.Add(new AttackModifier { amount = amount, turnsLeft = turns});
        UpdateStatusText();
    }

    public void ResetBlock()
    {
        block = 0;
        UpdateHealthBar();
    }

    public void PerformAttack()
    {
 //       Player.Instance.TakeDamage(ReduceAttackDamage(attackDamage));
    }

    private void Die()
    {
        Debug.Log("Enemy died");
        winScreen.SetActive(true);
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        UpdateHealthBar();
    }


    public void UpdateHealthBar()
    {
        enemySlider.value = health;
        healthText.text = $"{health}/ {maxHealth}";

        if (buff > 0)
            sliderFill.color = Color.yellow;

        if (attackDebuff > 0)
            sliderFill.color = Color.black;
    }


    private void UpdateStatusText() {
        if (statusText == null)
            return;

        int buffTotal = 0;
        int debuffTotal = 0;

        int buffMinTurns = int.MaxValue;
        int debuffMinTurns = int.MaxValue;

        foreach (AttackModifier mod in _attackMods)
        {
            if (mod.amount > 0) {
                buffTotal += mod.amount;
                buffMinTurns = Mathf.Min(buffMinTurns, mod.turnsLeft);
            } else if (mod.amount < 0) {
                debuffTotal += -mod.amount;
                debuffMinTurns = Mathf.Min(debuffMinTurns, mod.turnsLeft);
            }
        }

        System.Text.StringBuilder sb = new();

        if (buffTotal > 0)
            sb.AppendLine($"ATK +{buffTotal} ({buffMinTurns}T)");

        if (debuffTotal > 0)
            sb.AppendLine($"ATK - {debuffTotal} ({debuffMinTurns}T)");

        statusText.text = sb.ToString().TrimEnd();

    }
}
