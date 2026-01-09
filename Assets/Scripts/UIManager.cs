using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject slideSpot;

    private void Awake()
    {
        Instance = this;
    }
    public void SetActionMode(bool value)
    {
        Debug.Log("UI Action Mode = " + value);
    }
}
