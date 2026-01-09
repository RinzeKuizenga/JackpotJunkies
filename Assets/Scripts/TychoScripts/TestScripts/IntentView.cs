using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntentView : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text valueText;
    [SerializeField] private GameObject hiddenOverlay;
    
    [Header("Tooltip")]
    [SerializeField] private TooltipTrigger tooltipTrigger;

    [Header("Icon Library")]
    [SerializeField] private IntentIconLibrary iconLibrary;

    private IntentUIModel _current;

    // Roep deze method op met een model referentie om de intention te tonen.
    public void Render(IntentUIModel model) {
        _current = model;

        // Is voor de tooltip van de intention.
        if (hiddenOverlay != null)
            hiddenOverlay.SetActive(model.isHidden);

        if (iconImage != null) {
            Sprite icon = null;

            if (iconLibrary != null)
                icon = iconLibrary.GetIcon(model.type);

            iconImage.sprite = icon;
            iconImage.enabled = icon != null;
        }

        if (valueText != null) {
            bool showValue = !model.isHidden && model.value >= 0;
            valueText.gameObject.SetActive(showValue);

            if (showValue)
                valueText.text = model.value.ToString();
        }

        if (tooltipTrigger != null) {
            string title = model.isHidden ? "Unknown intent" : (string.IsNullOrEmpty(model.title) ? model.type.ToString() : model.title);
            string desc = model.isHidden ? "This enemy will do something next turn." : model.description;

            tooltipTrigger.SetText(title, desc);
        }
    }   

// Dit is een voorbeeld van hoe een IntentModel er uit moet zien. 
#if UNITY_EDITOR
    [ContextMenu("TEST/Render Attack 12")]
    private void TestAttack() {
        Render(new IntentUIModel{
                type = IntentType.Attack,
                value = 2,
                title = "Attack",
                description = "Deals damage to the player.",
                isHidden = false
                });
    }

    [ContextMenu("TEST/Render Hidden")]
    private void TestHidden() {
        Render(new IntentUIModel
                {
                    type = IntentType.Unknown,
                    value = -1,
                    title = "",
                    description = "",
                    isHidden = true
                });
    }
#endif
}
