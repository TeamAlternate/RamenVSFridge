using UnityEngine;
using UnityEngine.SceneManagement;

public class ForceQuit : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    public static void Initialize()
    {
        new GameObject().AddComponent<ForceQuit>();
        SceneManager.sceneLoaded += (_, _) => { new GameObject().AddComponent<ForceQuit>(); };
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Application Quit");
            Debug.Break();
            Application.Quit();
        }
    }
}
