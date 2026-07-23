using UnityEngine;
using Util.UserInterfaces;

namespace Sceces
{

    public class EndingManager : MonoBehaviour
    {
        [SerializeField] private KeyCode returnKey;
        [SerializeField] private KeyCode restartKey;
        [SerializeField] private SceneTransition toTitleTransitionPrefab;
        [SerializeField] private SceneTransition toMainGameTransitionPrefab;
        [SerializeField] private string titleSceneName;
        [SerializeField] private string mainGameSceneName;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            SceneTransition.ExitTransition();

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(returnKey))
            {
                ReturnToTitle();
            }
            if(Input.GetKeyDown(restartKey))
            {
                RestartGame();
            }
        }

        public void ReturnToTitle()
        {
            Instantiate(toTitleTransitionPrefab).EnterTransition(titleSceneName);
        }

        public void RestartGame()
        {
            Instantiate(toMainGameTransitionPrefab).EnterTransition(mainGameSceneName);
        }
    }
}
