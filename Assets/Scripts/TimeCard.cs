using UnityEngine;

public class TimeCard : CardBase
{
    public override void Play()
    {
        Vector3 scale = new Vector3(0.4f, 0.4f, 0.4f);
        this.transform.localScale = scale;
        this.GetComponent<CardDrag>().originalScale = scale;
    }
}
