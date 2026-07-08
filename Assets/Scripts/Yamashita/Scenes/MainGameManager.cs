using UnityEngine;

namespace Scenes
{

    public class MainGameManager : MonoBehaviour
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

        }

        public void MoveToEnding()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);

        }
    }

}