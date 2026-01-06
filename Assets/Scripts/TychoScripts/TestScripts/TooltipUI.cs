using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }

    [SerializeField] private GameObject root;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text bodyText;

    private void Awake() {
        Instance = this;
        Hide();
    }

    public void Show(string title, string body) {
        if (titleText != null) titleText.text = title;
        if (bodyText != null) bodyText.text = body;
        if (root != null) root.SetActive(true);
    }

    public void Hide() {
        if (root != null) root.SetActive(false);
    }
}
