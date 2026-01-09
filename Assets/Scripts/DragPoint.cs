using UnityEngine;

public class DragPoint : MonoBehaviour
{
    public int index;

    private void Start()
    {
        index = int.Parse(this.gameObject.name);
    }
}
