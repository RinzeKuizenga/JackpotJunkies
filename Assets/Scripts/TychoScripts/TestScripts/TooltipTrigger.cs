using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea] [SerializeField] private string title;
    [TextArea] [SerializeField] private string body;

    public void SetText(string newTitle, string newBody) {
        title = newTitle;
        body = newBody;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (TooltipUI.Instance != null) {
            Debug.Log("Test.");
            TooltipUI.Instance.Show(title, body);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (TooltipUI.Instance != null)
            TooltipUI.Instance.Hide();
    }
}
