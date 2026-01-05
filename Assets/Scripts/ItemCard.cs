using UnityEngine;

public class ItemCard : CardBase
{
    public override void Play()
    {
        Player.Instance.Heal(cardData.healAmount);
        Debug.Log($"Played ITEM card: healed {cardData.healAmount}");
    }
}
