using UnityEngine;
using Util.UserInterfaces;

public class NoPlayDetector : MonoBehaviour
{
    [SerializeField] private float timeoutDuration;
    [SerializeField] private SceneTransition sceneTransitionPrefab;
    [SerializeField] private string sceneName;
    private float elapsedTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        for (KeyCode k = KeyCode.JoystickButton0; k < KeyCode.Joystick8Button19; k++)
        {
            if(Input.GetKeyDown(k))
            {
                elapsedTime = 0.0f;
                return;
            }
        }

        if (elapsedTime > timeoutDuration)
        {
            Instantiate(sceneTransitionPrefab).EnterTransition(sceneName);
            this.enabled = false;
            return;
        }
    }
}
