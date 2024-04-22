using UnityEngine;
using UnityEngine.SceneManagement;

public class noplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Renderer renderer = GetComponent<Renderer>();

        if (sceneName == "Main Game")
        {
            renderer.enabled = false;
        }
        else if (sceneName == "HandTracking2")
        {
            renderer.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}