using UnityEngine;

public abstract class CardBase : MonoBehaviour, IPlayable
{
    public Card cardData; // jouw ScriptableObject

    public abstract void Play();
}
