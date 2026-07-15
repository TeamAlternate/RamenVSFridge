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
            CameraController.AddTarget(GameObject.FindWithTag("Ramen"));
            CameraController.AddTarget(GameObject.FindWithTag("Fridge"));
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void MoveToEnding()
        {
            Instantiate(toEndingTransitionPrefab).EnterTransition(endingSceneName);
        }
    }

}