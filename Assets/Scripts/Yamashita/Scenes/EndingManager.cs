using UnityEngine;

namespace Sceces
{

    public class EndingManager : MonoBehaviour
    {
        [SerializeField] private KeyCode returnKey;
        [SerializeField] private KeyCode restartKey;
        [SerializeField] private string nextSceneName;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

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
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        }

        public void RestartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);

        }
    }
}
