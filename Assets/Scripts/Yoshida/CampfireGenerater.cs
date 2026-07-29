using UnityEngine;

public class CampfireGenerater : MonoBehaviour
{
    [SerializeField] CampfireController campfirePrefab;

    private CampfireController campfire;
    [SerializeField] private float time = 0.0f;
    private float minIntervalTime = 15.0f;
    private float intervalRandomRange = 10.0f;

    private void Awake()
    {
        TimeReset();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if( time <= 0.0f)
        {
            if (campfire == null)
            {
                GenerateCampfire();
                TimeReset();
            }
        }
    }

    private void TimeReset()
    {
        time = minIntervalTime + Random.Range(0, intervalRandomRange);
    }

    private void GenerateCampfire()
    {
        campfire = Instantiate(campfirePrefab, this.transform);
    }
}
