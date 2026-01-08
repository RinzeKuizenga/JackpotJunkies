using System.Collections;
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
        StartCoroutine(EndTurnRoutine());
    }

    private IEnumerator EndTurnRoutine()
    {
        yield return TimelineManager.Instance.ExecuteTimeline();

        GameManager.Instance.ChangeState(
            new EnemyState(GameManager.Instance)
        );
    }

}
