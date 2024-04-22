using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class jiandang : MonoBehaviour
{
    public Image a1;  // 第一个页面
    public Image a2;  // 第二个页面
    public float tim;

    void Start()
    {
        tim = 0;
        a2.color = new Color(a2.color.r, a2.color.g, a2.color.b, 0);  // 初始时，第二个页面完全透明

    }

    void Update()
    {
        tim += Time.deltaTime;
        Color newa1 = a1.color;
        Color newa2 = a2.color;
        if (tim >= 1 && tim < 4)  // 第一个页面逐渐消失
        {
            newa1.a = 1 - Mathf.Clamp01((tim - 1) / 3);
        }
        else if (tim >= 4)  // 第一个页面完全消失后，第二个页面逐渐出现
        {
            newa1.a = 0;
            newa2.a = Mathf.Clamp01((tim - 4) / 3);
        }
        a1.color = newa1;
        a2.color = newa2;
    }
}
