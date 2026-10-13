using UnityEngine;
using Util.UserInterfaces;

public class ForceTransition : MonoBehaviour
{
    [SerializeField] private KeyCode invokeKey;

    [SerializeField] private SceneTransition sceneTransitionPrefab;
    [SerializeField] private string sceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(invokeKey))
        {
            Instantiate(sceneTransitionPrefab).EnterTransition(sceneName);
        }
    }
}
