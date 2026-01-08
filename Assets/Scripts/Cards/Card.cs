using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum CardType
{
    Attack,
    Defend,
    Item
}
public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Legendary
}

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public Sprite artwork;
    public Sprite background;
    public string description;

    public CardType type;
    public Rarity rarity;
    public int energyCost = 1;

    public int damageAmount;
    public int blockAmount;
    public int healAmount;
}
