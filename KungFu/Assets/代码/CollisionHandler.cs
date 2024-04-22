using System.Collections;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject kaishi; // 物体1
    public GameObject three; // 物体2
    public GameObject two; // 物体3
    public GameObject four;//物体四
    public GameObject five;//物体五
    public Animator Animator; // 物体1的动画组件
    public Animator Animator2; // 物体2的动画组件
    public AudioSource dianjiyinxiao; // 音频组件1
    public AudioSource CountDown; // 音频组件2
    public AudioSource TaijiBackgroundmusic; // 音频组件3

    private bool threeActive = false; // 物体2是否已经激活

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("1"))
        {
            Animator.SetInteger("aaa", 1); // 播放物体1的动画
            dianjiyinxiao.Play(); // Play the audio for object 1
            Invoke("a1", 3); // 调用a1方法
            Invoke("a2", 4); // 调用a2方法
            Invoke("a3", 5); // 调用a2方法
            Invoke("a4", 6); // 调用a4方法
            Invoke("a4_2", 10); // 调用a4方法
        }
    }
    public void a1()
    {
        kaishi.SetActive(false); // 关闭物体1
        three.SetActive(true); // 激活物体2
        CountDown.Play(); // Play the audio for object 2
    }
    public void a2()
    {
        three.SetActive(false); // 关闭物体2
        two.SetActive(true); // 激活物体3
    }
    public void a3()
    {
        two.SetActive(false); // 关闭物体3
        four.SetActive(true); // 激活物体4
    }
    public void a4()
    {
        four.SetActive(false); // 关闭物体4
        five.SetActive(true); // 激活物体5
        Animator2.SetInteger("bbb", 1); // 播放物体1的动画
    }
    public void a4_2()
    {
        five.SetActive(false); // 激活物体5
        TaijiBackgroundmusic.Play(); // Play the audio for object 3
    }

    IEnumerator ShowThreeAfterDelay()
    {
        yield return new WaitForSeconds(1f); // 等待1秒钟

        three.SetActive(false); // 关闭物体2

        yield return new WaitForSeconds(1f); // 等待1秒钟

        three.SetActive(true); // 激活物体3
        threeActive = true;
    }
}