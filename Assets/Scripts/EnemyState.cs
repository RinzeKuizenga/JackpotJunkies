using UnityEngine;

public class EnemyState : GameStateBase
{
    public EnemyState(GameManager gm) : base(gm) { }

    public override void Enter()
    {
        Debug.Log("ENTER: EnemyState");

        Enemy.Instance.PerformAttack();

        Player.Instance.ResetBlock();

        gm.ChangeState(new DrawState(gm));
    }
}
