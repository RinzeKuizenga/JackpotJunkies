using UnityEngine;

public class AttackCard : CardBase
{
    public override void Play()
    {
        Enemy.Instance.TakeDamage(cardData.damageAmount);
        Debug.Log("ATTACK");
    }
}
