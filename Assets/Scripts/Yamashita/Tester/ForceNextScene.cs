using UnityEngine;
using Util.UserInterfaces;

namespace _Tester
{

    public class ForceNextScene : MonoBehaviour
    {
        [SerializeField] private KeyCode nextKey;
        [SerializeField] private SceneTransition sceneTransitionPrefab;
        [SerializeField] private string nextSceneName;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(nextKey))
            {
                MatchResult result = new MatchResult()
                {
                    resultType = MatchResult.ResultTypes.RamenWin,

                };
                Scenes.MainGameManager.FinishGame(result);
            }
        }
    }
}
