using UnityEngine;

public class DefenseCard : CardBase
{
    public override void Play()
    {
        Player.Instance.AddBlock(cardData.blockAmount);
        Debug.Log($"Played DEFEND card: gained {cardData.blockAmount} block");
    }
}
