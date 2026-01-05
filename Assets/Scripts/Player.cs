using JetBrains.Annotations;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private int maxHealth = 50;
    [SerializeField] private int health;
    [SerializeField] private int block = 0;

    public Slider hpSlider;
    public TextMeshProUGUI blockText;
    public Image sliderFill;
    public TextMeshProUGUI healthText;  

    public int Health => health;
    public int Block => block;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        health = maxHealth;
        UpdateHPBar();
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

    public void AddBlock(int amount)
    {
        block += amount;
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
        else
        {
            sliderFill.color = Color.red;   
        }
    }
}


