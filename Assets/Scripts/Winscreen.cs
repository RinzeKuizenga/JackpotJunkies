using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Winscreen : MonoBehaviour
{
    public Button btn;
    public Animator animator;
    public GameObject WinScreen;

    private void Start()
    {
        animator = GetComponent<Animator>();
        WinScreen = gameObject;
        btn = GetComponentInChildren<Button>(true);
        btn.onClick.AddListener(WinscreenDissappear);
    }

    private void WinscreenDissappear()
    {
        animator.SetTrigger("End");
        StartCoroutine(WaitTime());
    }

    public IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1f);
        WinScreen.SetActive(false);
    }
}
