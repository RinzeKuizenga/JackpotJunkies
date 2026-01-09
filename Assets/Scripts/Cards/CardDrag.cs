using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))] 
public class CardDrag : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 startPosition;
    private Vector3 hoverScale;
    public Vector3 originalScale;
    public CardDisplay display;
    private CanvasGroup canvasGroup;
    private bool hovering = false;

    void Start()
    {
        display = GetComponent<CardDisplay>();
        canvasGroup = GetComponent<CanvasGroup>();

        startPosition = transform.position;
        originalScale = transform.localScale;
    }

    void Update()
    {
        hoverScale = originalScale * 1.1f;
        transform.localScale = Vector3.Lerp(transform.localScale, hovering ? hoverScale : originalScale, Time.deltaTime * 8);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.7f; 

        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        hovering = false;
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; 
        canvasGroup.alpha = 1f;

        bool isInSpot = false;

        foreach (GameObject point in TimeLineManager.instance.DragPoints)
        {
            RectTransform rect = point.GetComponent<RectTransform>();

            if (RectTransformUtility.RectangleContainsScreenPoint(rect, eventData.position, null))
            {
                TimeLineManager.instance.AddCard(display.card, int.Parse(point.name));
                transform.position = point.transform.position;
                isInSpot = true;
                break;
            }
        }

        if (isInSpot)
        {
            CardBase cardBase = GetComponent<CardBase>();
            if (cardBase != null) cardBase.Play();
        }
        else
        {
            transform.position = startPosition;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) { hovering = true; }
    public void OnPointerExit(PointerEventData eventData) { hovering = false; }
}