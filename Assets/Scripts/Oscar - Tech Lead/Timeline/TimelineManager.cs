using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimelineManager : MonoBehaviour
{
    public static TimelineManager Instance;

    [Header("Enemy Cards")]
    public List<Card> enemyAttackCards;
    public List<Card> enemyDefendCards;
    public List<Card> enemyHealCards;
    
    public TimelineSlot[] slots; // size = 6
    public GameObject timelineCardPrefab; // prefab met CardDisplay

    private void Awake()
    {
        Instance = this;
        ResetTimeline();
    }


    public void ResetTimeline()
    {
        foreach (TimelineSlot slot in slots)
        {
            slot.ResetSlot();
        }
    }

    
    // wordt aangeroepen vanuit CardDrag
    public bool TryPlaceCard(CardBase card, Vector2 mouseScreenPos)
    {
        int cost = card.cardData.energyCost;

        if (!Player.Instance.HasEnoughEnergy(cost))
            return false;

        for (int i = 0; i < slots.Length; i++)
        {
            TimelineSlot slot = slots[i];

            if (!slot.IsEmpty || slot.owner != SlotOwner.None)
                continue;

            RectTransform rt = slot.GetComponent<RectTransform>();
            if (RectTransformUtility.RectangleContainsScreenPoint(rt, mouseScreenPos))
            {
                PlaceCardInSlot(slot, card);
                Player.Instance.SpendEnergy(cost); // 👈 energie afschrijven
                return true;
            }
        }

        return false;
    }


    private void PlaceCardInSlot(TimelineSlot slot, CardBase cardBase)
    {
        slot.SetCard(cardBase.cardData, SlotOwner.Player);

        GameObject uiCard = Instantiate(timelineCardPrefab, slot.contentParent);
        CardDisplay display = uiCard.GetComponent<CardDisplay>();
        display.card = cardBase.cardData;
    }


    public IEnumerator ExecuteTimeline()
    {
        bool first = true;

        foreach (TimelineSlot slot in slots)
        {
            if (slot.storedCardData == null)
                continue;

            if (!first)
                yield return new WaitForSeconds(2f);

            first = false;

            ExecuteSlot(slot);
            RemoveSlotVisual(slot); // 👈 FIX 3
        }

        ClearTimeline();
    }

    private void RemoveSlotVisual(TimelineSlot slot)
    {
        slot.RemoveVisual();
    }


    private void ExecuteSlot(TimelineSlot slot)
    {
        Card card = slot.storedCardData;

        if (slot.owner == SlotOwner.Player)
        {
            ExecutePlayerCard(card);
        }
        else if (slot.owner == SlotOwner.Enemy)
        {
            ExecuteEnemyCard(card);
        }
    }

    
    private void ExecutePlayerCard(Card card)
    {
        switch (card.type)
        {
            case CardType.Attack:
                Enemy.Instance.TakeDamage(card.damageAmount);
                break;

            case CardType.Defend:
                Player.Instance.AddBlock(card.blockAmount);
                break;

            case CardType.Item:
                Player.Instance.Heal(card.healAmount);
                break;
        }
    }

    private void ExecuteEnemyCard(Card card)
    {
        switch (card.type)
        {
            case CardType.Attack:
                Player.Instance.TakeDamage(card.damageAmount);
                break;

            case CardType.Defend:
                Enemy.Instance.AddBlock(card.blockAmount); // moet je hebben
                break;

            case CardType.Item:
                Enemy.Instance.Heal(card.healAmount);
                break;
        }
    }



    private void ExecuteCard(Card card)
    {
        switch (card.type)
        {
            case CardType.Attack:
                Enemy.Instance.TakeDamage(card.damageAmount);
                break;

            case CardType.Defend:
                Player.Instance.AddBlock(card.blockAmount);
                break;

            case CardType.Item:
                Player.Instance.Heal(card.healAmount);
                break;
        }
    }

    
    private void ClearTimeline()
    {
        foreach (TimelineSlot slot in slots)
        {
            slot.Clear();
        }
    }
    
    public void GenerateEnemyTimeline() {
        int actions = 2;

        List<int> freeSlots = new List<int>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty)
                freeSlots.Add(i);
        }

        for (int i = 0; i < actions && freeSlots.Count > 0; i++)
        {
            int slotIndex = freeSlots[Random.Range(0, freeSlots.Count)];
            freeSlots.Remove(slotIndex);

            Card enemyCard = GetRandomEnemyCard();
            PlaceEnemyCard(slots[slotIndex], enemyCard);
        }
    }

    private Card GetRandomEnemyCardWithRarity(List<Card> cards)
    {
        List<Card> weighted = new();

        foreach (Card card in cards)
        {
            int weight = card.rarity switch
            {
                Rarity.Common => 5,
                Rarity.Uncommon => 3,
                Rarity.Rare => 2,
                Rarity.Legendary => 1,
                _ => 1
            };

            for (int i = 0; i < weight; i++)
                weighted.Add(card);
        }

        return weighted[Random.Range(0, weighted.Count)];
    }

    
    private Card GetRandomEnemyCard()
    {
        int roll = Random.Range(0, 3);

        switch (roll)
        {
            case 0:
                return GetRandomEnemyCardWithRarity(enemyAttackCards);

            case 1:
                return GetRandomEnemyCardWithRarity(enemyDefendCards);

            case 2:
                return GetRandomEnemyCardWithRarity(enemyHealCards);
        }

        return null;
    }


    private void PlaceEnemyCard(TimelineSlot slot, Card card)
    {
        slot.SetCard(card, SlotOwner.Enemy);

        GameObject uiCard = Instantiate(timelineCardPrefab, slot.contentParent);
        CardDisplay display = uiCard.GetComponent<CardDisplay>();
        display.card = card;
        
        display.artwork.color = new Color(1f, 0.5f, 0.5f, 1f);     // rood-tint
        display.background.color = new Color(0.8f, 0.3f, 0.3f, 1f);
    }

}