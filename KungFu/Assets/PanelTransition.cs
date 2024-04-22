using UnityEngine;
using System.Collections;

public class PanelTransition : MonoBehaviour
{
    public CanvasGroup canvasGroup; // CanvasGroup 用于控制面板的透明度

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>(); // 获取面板上的 CanvasGroup 组件
    }

    // 渐隐面板
    public void FadeOut()
    {
        // 使用 Unity 自带的动画系统
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0f, 1f));
    }

    // 显示面板
    public void ShowPanel()
    {
        canvasGroup.alpha = 1f; // 设置透明度为完全可见
        gameObject.SetActive(true); // 启用面板
    }

    // 使用协程实现 CanvasGroup 渐隐效果
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cg.alpha = end;

        if (end == 0f)
        {
            gameObject.SetActive(false); // 渐隐完成后禁用面板
        }
    }
}
