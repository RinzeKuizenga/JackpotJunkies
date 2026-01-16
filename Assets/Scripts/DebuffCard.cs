using UnityEngine;

public class DebuffCard : CardBase
{
    public override void Play() {
        Enemy.Instance.ApplyAttackDebuff(cardData.debuffAmount);
    }
}
