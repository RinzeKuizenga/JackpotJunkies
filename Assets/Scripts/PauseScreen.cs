using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private GameObject pauseUI;
    [SerializeField] private Animator animator;
    private bool isPaused;

    private void Awake()
    {
        pauseUI = transform.GetChild(0).gameObject;
        animator = pauseUI.GetComponent<Animator>();

        pauseUI.SetActive(true); 
        animator.SetBool("IsPaused", false);
        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        animator.SetBool("IsPaused", isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
