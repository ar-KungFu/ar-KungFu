using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerhand : MonoBehaviour
{
    public GameObject circleHand; // 圆手掌对象
    private Animator circleHandAnimator; // 圆手掌对象上的Animator组件

    private void Start()
    {
        // 获取圆手掌对象上的Animator组件
        circleHandAnimator = circleHand.GetComponent<Animator>();

        if (circleHandAnimator == null)
        {
            Debug.LogError("Animator component not found on circleHand object.");
        }
        else if (!circleHandAnimator.enabled)
        {
            Debug.LogError("Animator component is disabled on circleHand object.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞对象是否为圆手掌
        if (collision.gameObject == circleHand)
        {
            // 触发hand anim3动画
            circleHandAnimator.SetTrigger("hand");
        }
    }
}