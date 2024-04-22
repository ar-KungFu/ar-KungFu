using UnityEngine;
using UnityEngine.SceneManagement;

public class tiaozhuan : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("taijidiyishi")
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            SceneManager.LoadScene("HandTracking1");
        }
    }
}