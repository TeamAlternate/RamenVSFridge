using UnityEngine;
using Util.UserInterfaces;

namespace Scenes
{

    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private SceneTransition toMainGameTransitionPrefab;
        [SerializeField] private string mainGameSceneName;

        private void Awake()
        {
            // èâä˙âª
            MatchSettings.recent = new MatchSettings() { ramenPlayer = default, fridgePlayer = default };
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            SceneTransition.ExitTransition();
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void StartGame()
        {
            PlayerJoinManager playerJoinManager = GameObject.Find("GameManager").GetComponent<PlayerJoinManager>();
            MatchSettings.Update(new MatchSettings() { ramenPlayer = playerJoinManager.playerDevice[0], fridgePlayer = playerJoinManager.playerDevice[1] });
            Instantiate(toMainGameTransitionPrefab).EnterTransition(mainGameSceneName);
        }
    }
}
