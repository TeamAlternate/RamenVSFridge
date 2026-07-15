using UnityEngine;
using Util.UserInterfaces;

namespace Scenes
{

    public class MainGameManager : MonoBehaviour
    {
        [SerializeField] private KeyCode startKey;
        [SerializeField] private SceneTransition toEndingTransitionPrefab;
        [SerializeField] private string endingSceneName;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            SceneTransition.ExitTransition();
            // CameraController.AddTarget(GameObject.FindWithTag("Ramen"));
            //CameraController.AddTarget(GameObject.FindWithTag("Fridge"));
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(startKey))
            {
                MoveToEnding();
            }
        }

        public void MoveToEnding()
        {
            Instantiate(toEndingTransitionPrefab).EnterTransition(endingSceneName);
        }
    }

}