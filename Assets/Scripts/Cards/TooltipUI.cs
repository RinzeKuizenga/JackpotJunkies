using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI instance;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;

    CanvasGroup group;

    void Awake()
    {
        instance = this;
        group = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        group.alpha = 0;
        group.blocksRaycasts = false;
    }

    public void Show(string name, string desc)
    {
        nameText.text = name;
        descText.text = desc;
        group.alpha = 1;
    }

    public void Hide()
    {
        group.alpha = 0;
    }
}
