using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private int maxHealth = 50;
    [SerializeField] private int health;
    [SerializeField] private int block = 0;
    [SerializeField] private int maxEnergy = 30;
    [SerializeField] private int energy;
    [SerializeField] private int buff = 0;

    public Slider hpSlider;
    public TextMeshProUGUI blockText;
    public Image sliderFill;
    public TextMeshProUGUI healthText;  

    public int Health => health;
    public int Block => block;
    public int Buff => buff;

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

        UpdateHPBar();

        if (health <= 0)
        {
            health = 0;
            OnPlayerDeath();
        }
    }

    public int GetAttackDamage(int baseDamage) {
        return baseDamage + buff;
    }

    public void AddBlock(int amount)
    {
        block += amount;
        UpdateHPBar();
    }

    public void AddBuff(int amount) {
        buff += amount;
        UpdateHPBar();
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
        if (block > 0)
        {
            sliderFill.color = Color.blue;
        }
        Debug.Log($"Buff amount: {buff}");
        if (buff > 0) {
            sliderFill.color = Color.yellow;
        }
        else
        {
            sliderFill.color = Color.red;   
        }
    }
}


