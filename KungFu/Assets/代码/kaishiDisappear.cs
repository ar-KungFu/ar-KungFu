using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class kaishiDisappear : MonoBehaviour
{
    public Animator Animator;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "kaishi")
        {
            Animator.SetTrigger("kaishiDisappear");
        }
    }
}