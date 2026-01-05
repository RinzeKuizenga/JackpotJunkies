using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameStateBase currentState;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeState(new DrawState(this));
    }

    private void Update()
    {
        currentState?.Tick();
    }

    public void ChangeState(GameStateBase newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}
