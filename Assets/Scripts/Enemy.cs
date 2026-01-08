using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;

    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int health;
    [SerializeField] private int block = 0;
    [SerializeField] private int attackDamage = 6;

    public Slider enemySlider;
    public TextMeshProUGUI healthText;

    public int Health => health;
    public int Block => block;
    public int AttackDamage => attackDamage;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        health = maxHealth;
        UpdateHealthBar();
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

    public void ResetBlock()
    {
        block = 0;
        UpdateHealthBar();
    }

    public void PerformAttack()
    {
        Player.Instance.TakeDamage(attackDamage);
    }

    private void Die()
    {
        Debug.Log("Enemy died");
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
    }
}