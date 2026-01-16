using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private int maxHealth = 50;
    [SerializeField] private int health;
    [SerializeField] private int block = 0;
    [SerializeField] private int maxEnergy = 30;
    [SerializeField] private int energy;
    [SerializeField] private int buff = 0;
    [SerializeField] private int attackDebuff = 0;

    public Slider hpSlider;
    public TextMeshProUGUI blockText;
    public Image sliderFill;
    public TextMeshProUGUI healthText;  
    public TextMeshProUGUI statusText;

    public int Health => health;
    public int Block => block;
    public int Buff => buff;

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
        energy = 10;
        UpdateEnergyUI();
        UpdateHPBar();
    }
    
    
    public bool HasEnoughEnergy(int cost)
    {
        return energy >= cost;
    }

    public void SpendEnergy(int cost)
    {
        energy -= cost;
        if (energy < 0) energy = 0;
    }

    public void ResetEnergy() {
        energy =+ Random.Range(1, 10);
    }

    
    private void UpdateEnergyUI()
    {
        Debug.Log($"Energy: {energy}/{maxEnergy}");
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


        if (health <= 0)
        {
            health = 0;
            OnPlayerDeath();
        }
    }

    public int CalculateOutgoingAttackDamage(int baseDamage) {
        int totalMod = 0;
        foreach (AttackModifier m in _attackMods) totalMod += m.amount;
        return Mathf.Max(0, baseDamage + totalMod);
    }

    public void AddBlock(int amount)
    {
        block += amount;
        UpdateHPBar();
    }

    public void AddBuff(int amount, int turns = 3) {
        amount = Mathf.Max(0, amount);
        turns = Mathf.Max(1, turns);
        _attackMods.Add(new AttackModifier {amount = amount, turnsLeft = turns});
        UpdateStatusText();
    }

    public void ApplyAttackDebuff(int amount, int turns = 3) {
        amount = Mathf.Max(0, amount);
        turns = Mathf.Max(1, turns);
        _attackMods.Add(new AttackModifier {amount = -amount, turnsLeft = turns});
        UpdateStatusText();
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

    public void ResetBlock()
    {
        block = 0;
        UpdateHPBar();
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        UpdateHPBar();
    }

    private void OnPlayerDeath()
    {
        Debug.Log("DEAD");
    }

    private void UpdateHPBar()
    {
        hpSlider.value = health;
        blockText.text = block.ToString();
        healthText.text = $"{health}/ 50";
        
        if (buff > 0) {
            sliderFill.color = Color.yellow;
        }

        if (block > 0)
        {
            sliderFill.color = Color.blue;
        }

        if (attackDebuff > 0) {
            sliderFill.color = Color.black;
        }
        else
        {
            sliderFill.color = Color.red;   
        }
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


