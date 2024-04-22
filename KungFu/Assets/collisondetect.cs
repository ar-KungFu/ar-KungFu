using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisondetect : MonoBehaviour
{
    public Animator animator; // 动画播放器

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 播放动画
            animator.Play("圆手掌");
        }
    }
}