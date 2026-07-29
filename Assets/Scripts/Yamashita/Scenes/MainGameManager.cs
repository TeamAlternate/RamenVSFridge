using UnityEngine;
using Util.UserInterfaces;

namespace Scenes
{

    public class MainGameManager : MonoBehaviour
    {
        [SerializeField] private KeyCode startKey;
        [SerializeField] private SceneTransition toEndingTransitionPrefab;
        [SerializeField] private string endingSceneName;

        [SerializeField] private string[] cameraFocusTags;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            SceneTransition.ExitTransition();
            foreach(string tag in cameraFocusTags)
            {
                foreach(GameObject obj in GameObject.FindGameObjectsWithTag(tag))
                {
                    CameraController.AddTarget(obj);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(startKey))
            {
                FinishGame();
            }
        }

        public void FinishGame()
        {
            MatchResult result = new MatchResult()
            {
                resultType = MatchResult.ResultTypes.RamenWin,

            };
            MatchResult.Update(result);
            MoveToEnding();
        }

        public void MoveToEnding()
        {
            Instantiate(toEndingTransitionPrefab).EnterTransition(endingSceneName);
        }
    }

}