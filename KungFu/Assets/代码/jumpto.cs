using UnityEngine;
using UnityEngine.SceneManagement;

public class jumpto : MonoBehaviour
{
    public string HandTracking; // 目标场景的名称

    // 当按钮被点击时调用
    public void LoadTargetScene()
    {
        SceneManager.LoadScene(HandTracking); // 加载目标场景
    }
}

