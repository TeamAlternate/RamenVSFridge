using UnityEngine;

public class CommonGameObjectSettings : MonoBehaviour
{
    [SerializeField] private bool isDontDestroyOnLoad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(isDontDestroyOnLoad)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
