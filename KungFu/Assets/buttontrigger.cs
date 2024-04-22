using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttontrigger : MonoBehaviour
{
    public GameObject thirdPanel; // 将第三个面板拖到这个public变量上

    // Start is called before the first frame update
    void Start()
    {
        // 初始时，第三个面板不可见
        thirdPanel.SetActive(false);
    }

    public void ShowThirdPanel()
    {
        // 显示第三个面板
        thirdPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
