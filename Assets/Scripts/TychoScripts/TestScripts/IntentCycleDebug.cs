using UnityEngine;

public class IntentCycleDebug : MonoBehaviour
{
    [SerializeField] private IntentView intentView;
    [SerializeField] private float secondsPerTurn = 2.0f;

    private float timer;
    private int index;

    private void Reset() {
        if (intentView == null)
            intentView = GetComponent<IntentView>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (intentView != null)
            intentView.Render(GetDemoIntent(0));
    }

    // Update is called once per frame
    void Update()
    {
        if (intentView == null) return;

        timer += Time.deltaTime;
        if (timer >= secondsPerTurn) {
            timer = 0f;
            index = (index + 1) % 6;
            intentView.Render(GetDemoIntent(index));
        }
    }

    private IntentUIModel GetDemoIntent(int i) {
        switch (i)
        {
            case 0:
                return new IntentUIModel { type = IntentType.Attack, value = 12, title = "Attack", description = "This enemy intends to deal damage.", isHidden = false};
            case 1:
                return new IntentUIModel { type = IntentType.Defend, value = 8, title = "Defend", description = "This enemy is preparing to block.", isHidden = false};
            case 2:
                return new IntentUIModel { type = IntentType.Buff, value = -1, title = "Buff", description = "This enemy will strengthen itself.", isHidden = false};
            case 3:
                return new IntentUIModel { type = IntentType.Debuff, value = -1, title = "Debuff", description = "This enemy plans to weaken the player.", isHidden = false};
            case 4:
                return new IntentUIModel { type = IntentType.Special, value = -1, title = "Special", description = "This enemy will perform a special action.", isHidden = false};
            default:
                return new IntentUIModel { type = IntentType.Unknown, value = -1, title = "", description = "", isHidden = true};
        }
    }
}
