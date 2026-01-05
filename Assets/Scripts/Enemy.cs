using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;

    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int health;
    [SerializeField] private int attackDamage = 6;

    public Slider enemySlider;
    public TextMeshProUGUI healthText;

    public int Health => health;
    public int AttackDamage => attackDamage;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        UpdateHealthBar();

        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void PerformAttack()
    {
        Player.Instance.TakeDamage(attackDamage);
    }

    private void Die()
    {

    }

    public void UpdateHealthBar()
    {
        enemySlider.value = health;
        healthText.text = $"{health}/ 30";
    }
}
