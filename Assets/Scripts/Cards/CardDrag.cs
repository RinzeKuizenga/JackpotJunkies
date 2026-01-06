using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 startPosition;
    private Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f);
    public Vector3 originalScale;

    public GameObject card;
    public GameObject darkness;
    public GameObject slideSpot;



    private bool hovering = false;

    public void Init(GameObject darknessObj, GameObject slideSpotObj)
    {
        darkness = darknessObj;
        slideSpot = slideSpotObj;
    }

    void Start()
    {
        card = gameObject;

        if (darkness == null)
            darkness = UIManager.Instance.darkness;

        if (slideSpot == null)
            slideSpot = UIManager.Instance.slideSpot;

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
        originalScale = new Vector3(1, 1, 1);
        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        hovering = false;
        Vector3 mousePos = Input.mousePosition;

        darkness.SetActive(true);
        transform.position = new Vector3(mousePos.x, mousePos.y, mousePos.z);
        transform.localScale = Vector3.one;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform slideRect = slideSpot.GetComponent<RectTransform>();

        bool isInSpot = RectTransformUtility.RectangleContainsScreenPoint(slideRect, Input.mousePosition);

        darkness.SetActive(false);
        if (isInSpot)
        {
            CardBase cardBase = GetComponent<CardBase>();
            cardBase.Play();
        }
        else
        {
            transform.position = startPosition;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }
}
