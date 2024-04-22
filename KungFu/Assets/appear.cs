using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 引入命名空间

public class appear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 获取当前的场景
        Scene currentScene = SceneManager.GetActiveScene();

        // 如果当前的场景是 "main game"，那么隐藏这个游戏对象
        if (currentScene.name == "Main Game")
        {
            gameObject.SetActive(false);
        }
        // 如果当前的场景是 "20240407"，那么显示这个游戏对象
        else if (currentScene.name == "20240407")
        {
            gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}