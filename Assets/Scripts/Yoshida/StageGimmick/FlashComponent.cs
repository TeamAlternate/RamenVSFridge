using UnityEngine;

public class FlashComponent : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;

    bool isActive = false;
    float flashInterval = 0.1f;
    float flashIntervalTime = 0.0f;

    public void RunFlash()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if( isActive )
        {
            if (flashIntervalTime < 0.0f)
            {
                foreach (GameObject obj in objects)
                {
                    if (obj.GetComponent<MeshRenderer>().enabled == false)
                    {
                        obj.GetComponent<MeshRenderer>().enabled = true;
                    }
                    else
                    {
                        obj.GetComponent<MeshRenderer>().enabled = false;
                    }
                }
                flashIntervalTime = flashInterval;
            }
            else
            {
                flashIntervalTime -= Time.deltaTime;
            }
        }
    }
}
