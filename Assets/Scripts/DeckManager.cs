using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;

    public List<Card> allCards;

    public List<Card> commons = new List<Card>();
    public List<Card> uncommons = new List<Card>();
    public List<Card> rares = new List<Card>();
    public List<Card> legendaries = new List<Card>();

    public Transform handArea;
    public GameObject attackPrefab;
    public GameObject defendPrefab;
    public GameObject itemPrefab;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SortCards();
    }

    private void SortCards()
    {
        foreach (Card card in allCards)
        {
            switch (card.rarity)
            {
                case Rarity.Common:
                    commons.Add(card);
                    break;
                case Rarity.Uncommon:
                    uncommons.Add(card);
                    break;
                case Rarity.Rare:
                    rares.Add(card);
                    break;
                case Rarity.Legendary:
                    legendaries.Add(card);
                break;
            }
        }
    }

    public void Draw(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            DrawRandomCard();
        }
    }

    private void DrawRandomCard()
    {
        Rarity rarity = GetRandomRarity();
        Card chosen = null;
        GameObject prefabToUse = null;

        if (rarity == Rarity.Common && commons.Count > 0)
        {
            chosen = commons[Random.Range(0, commons.Count)];
        }
        else if (rarity == Rarity.Uncommon && uncommons.Count > 0)
        {
            chosen = uncommons[Random.Range(0, uncommons.Count)];
        }
        else if (rarity == Rarity.Rare && rares.Count > 0)
        {
            chosen = rares[Random.Range(0, rares.Count)];
        }
        else if (rarity == Rarity.Legendary && legendaries.Count > 0)
        {
            chosen = legendaries[Random.Range(0, legendaries.Count)];
        }


        switch (chosen.type)
        {
            case CardType.Attack:
                prefabToUse = attackPrefab;
                break;
            case CardType.Defend:
                prefabToUse = defendPrefab;
                break;
            case CardType.Item:
                prefabToUse = itemPrefab;
                break;
        }

        GameObject newCard = Instantiate(prefabToUse, handArea);
        newCard.GetComponent<CardBase>().cardData = chosen;
        newCard.GetComponent<CardDisplay>().card = chosen;

        CardDrag drag = newCard.GetComponent<CardDrag>();
        if (drag != null)
        {
            drag.Init(UIManager.Instance.darkness, UIManager.Instance.slideSpot);
        }

        Debug.Log("Drew card: " + chosen.cardName);
    }
    private Rarity GetRandomRarity()
    {
        int roll = Random.Range(0, 100);

        if (roll < 70)
        {
            return Rarity.Common;
        }
        if (roll < 90)
        {

            return Rarity.Uncommon;
        }
        if (roll < 99)
        {
            return Rarity.Rare;
        }
        return Rarity.Legendary;
    }


}
