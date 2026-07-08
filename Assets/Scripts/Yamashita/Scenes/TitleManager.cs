using UnityEngine;

namespace Scenes
{

    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private KeyCode startKey;
        [SerializeField] private string nextSceneName;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

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
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        }
    }
}
