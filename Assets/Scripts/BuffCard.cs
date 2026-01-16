using UnityEngine;

public class BuffCard : CardBase
{
    public override void Play() {
        Player.Instance.AddBuff(cardData.buffAmount);
        Debug.Log($"Played DEFEND card: gained {cardData.blockAmount} block");
    }
}
