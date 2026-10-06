using UnityEngine;

namespace Sounds
{

    public class BGMPlayer : MonoBehaviour
    {
        private static BGMPlayer instance;
        [SerializeField] private AudioSource audioSource;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
