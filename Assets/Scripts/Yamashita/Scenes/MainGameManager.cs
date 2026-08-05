using System.Collections;
using UnityEngine;
using Util.UserInterfaces;

namespace Scenes
{

    public class MainGameManager : MonoBehaviour
    {
        private static MainGameManager instance;

        [SerializeField] private TimeManager timeManager;
        [SerializeField] private SceneTransition toEndingTransitionPrefab;
        [SerializeField] private string endingSceneName;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            SceneTransition.ExitTransition();
            void InternalRoutine()
            {
                IEnumerator Internal2()
                {

                yield return new WaitForSeconds(2.0f);
                FinishGame(new MatchResult() { resultType = MatchResult.ResultTypes.FridgeWin});
                }
                StartCoroutine(Internal2());
            }
            timeManager.OnTimeup += InternalRoutine;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public static void FinishGame(MatchResult result)
        {
            MatchResult.Update(result);
            instance.MoveToEnding();
        }

        public void MoveToEnding()
        {
            Instantiate(toEndingTransitionPrefab).EnterTransition(endingSceneName);
        }
    }

}