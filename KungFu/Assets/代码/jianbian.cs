using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class jianbian : MonoBehaviour
{
    // Start is called before the first frame update
    public Image a2;
    public Image a3;
    public float timm;
    public static bool w1 = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        q1();
    }

    public void q1()
    {
        if (w1)
        {
            timm += Time.deltaTime;
            Color newa2 = a2.color;
            newa2.a = 0 + Mathf.Clamp01(timm / 2);
            a2.color = newa2;
            a3.color = newa2;
            if (timm >= 2)
            {
                enabled = false;
            }
        }
    }
}
