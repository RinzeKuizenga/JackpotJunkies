using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void Continue() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Feature-TimelineSystem");
    }
}
