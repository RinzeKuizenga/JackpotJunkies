using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public TextMeshProUGUI cardName;
    public TextMeshProUGUI description;

    public Image artwork;
    public Image background;

    void Start()
    {
        cardName.text = card.cardName;
        description.text = card.description;    
        artwork.sprite = card.artwork;  
        background.sprite = card.background;
    }
}
