using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // 获取被撞击的物体上的Animator组件
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // 播放动画
            animator.Play("hand anim3");
        }
    }
}