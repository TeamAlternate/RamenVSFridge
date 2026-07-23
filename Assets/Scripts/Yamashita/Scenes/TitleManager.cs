using UnityEngine;
using Util.UserInterfaces;

namespace Scenes
{

    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private KeyCode startKey;
        [SerializeField] private SceneTransition toMainGameTransitionPrefab;
        [SerializeField] private string mainGameSceneName;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            SceneTransition.ExitTransition();

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(startKey))
            {
                StartGame();
            }
        }

        public void StartGame()
        {
            MatchSettings.Update(new MatchSettings());
            Instantiate(toMainGameTransitionPrefab).EnterTransition(mainGameSceneName);
        }
    }
}
