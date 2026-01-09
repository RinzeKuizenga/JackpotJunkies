using UnityEngine;

public class SceneManager : MonoBehaviour
{   
    public void Tooltip()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EnemyIntentionsScene");
    }

    public void WinScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Winscreen");
    }
    
    public void TimelineSystem()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Feature-TimelineSystem");
    }
}
