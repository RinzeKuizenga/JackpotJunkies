using UnityEngine;
using UnityEngine.EventSystems;

public class CardTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    CardDisplay cardDisplay;
    bool hovering;

    void Start()
    {
        cardDisplay = GetComponent<CardDisplay>();
    }

    void Update()
    {
        if (!hovering)
        {
            return;
        }

        if (!Input.GetKeyDown(KeyCode.T))
        {
            return;
        }

        TooltipUI.instance.Show(
            cardDisplay.card.cardName,
            cardDisplay.card.description
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        TooltipUI.instance.Hide();
    }
}
