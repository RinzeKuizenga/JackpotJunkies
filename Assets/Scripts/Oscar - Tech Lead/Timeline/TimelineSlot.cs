using UnityEngine;

public enum SlotOwner
{
    None,
    Player,
    Enemy
}

public class TimelineSlot : MonoBehaviour
{
    public Card storedCardData;
    public SlotOwner owner = SlotOwner.None;

    public Transform contentParent;

    public bool IsEmpty => storedCardData == null;

    public void SetCard(Card card, SlotOwner newOwner)
    {
        storedCardData = card;
        owner = newOwner;
    }

    public void Clear()
    {
        storedCardData = null;
        owner = SlotOwner.None;

        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
    }
    
    public void ResetSlot()
    {
        storedCardData = null;
        owner = SlotOwner.None;

        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void RemoveVisual()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
    }

    
}