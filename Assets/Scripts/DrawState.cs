using UnityEngine;
using System.Collections;

using UnityEngine;

public class DrawState : GameStateBase
{
    public DrawState(GameManager gm) : base(gm) { }

    public override void Enter() {
        TimelineManager.Instance.ResetTimeline();

        Player.Instance.ResetEnergy(); // 👈 HIER

        int currentHand = DeckManager.Instance.CardsInHand();
        int cardsToDraw = Mathf.Max(0, 5 - currentHand);
        DeckManager.Instance.Draw(cardsToDraw);

        TimelineManager.Instance.GenerateEnemyTimeline();
        gm.ChangeState(new ActionState(gm));
    }
}

