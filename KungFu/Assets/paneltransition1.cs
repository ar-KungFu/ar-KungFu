using UnityEngine;
using UnityEngine.UI;

public class paneltransition1 : MonoBehaviour
{
    public GameObject thirdPanel; // 第三个面板的引用

    // 当按钮被点击时调用，显示第三个面板
    public void ShowThirdPanel()
    {
        thirdPanel.SetActive(true); // 激活第三个面板，使其可见
    }
}
