using UnityEngine;

public abstract class GameStateBase
{
    protected GameManager gm;

    public GameStateBase(GameManager gameManager)
    {
        gm = gameManager;
    }

    public virtual void Enter() { }
    public virtual void Tick() { }
    public virtual void Exit() { }
}


