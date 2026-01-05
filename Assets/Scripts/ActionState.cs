using UnityEngine;

public class ActionState : GameStateBase
{
    public ActionState(GameManager gm) : base(gm) { }

    public override void Enter()
    {
        Debug.Log("ENTER: ActionState");
    }
}

