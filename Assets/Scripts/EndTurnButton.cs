using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(EndTurn);
    }

    private void EndTurn()
    {
        Debug.Log("END TURN pressed!");

        GameManager.Instance.ChangeState(
            new EnemyState(GameManager.Instance)
        );
    }
}
