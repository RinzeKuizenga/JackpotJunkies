using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TimeLineManager : MonoBehaviour
{
    public static TimeLineManager instance;
    public GameObject[] DragPoints;
    public List<CardInTimeline> cards = new List<CardInTimeline>();

    public void Start()
    {
        instance = this;

        for (int i = 0; i < 11; i++)
        {
            cards.Add(new CardInTimeline(i));
        }

    }
    public void AddCard(Card card, int pos)
    {
        cards[pos].cards.Add(card);
    }
}
[Serializable]
public class CardInTimeline
{
    int position { get;  }
    Card currentCard { get; }

    public List<Card> cards = new List<Card>();

    public CardInTimeline(int pos)
    {
        position = pos;
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }
}
