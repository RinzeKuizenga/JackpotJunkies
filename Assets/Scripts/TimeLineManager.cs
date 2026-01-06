using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineManager : MonoBehaviour
{
    public static TimeLineManager instance;
    public GameObject[] DragPoints;
    List<CardInTimeline> cards = new List<CardInTimeline>();

    public void Start()
    {
        instance = this;
    }

    public void AddCard(Card card, int pos)
    {
        cards.Add(new CardInTimeline(card, pos));
    }
}
[Serializable]
public class CardInTimeline
{
    int position { get;  }
    Card currentCard { get; }

    public CardInTimeline(Card card, int pos)
    {
        currentCard = card;
        position = pos;
    }
}
