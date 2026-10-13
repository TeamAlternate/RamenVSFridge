using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleGenerater : MonoBehaviour
{
    const float GENERATE_OFFSET = 10.0f;
    const float TIMERANGE_MIN = 45.0f;
    const float TIMERANGE_MAX = 60.0f;

    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float lifeTime = 20.0f;

    [SerializeField] float generateTime = 0.0f;
    bool isGenerated = false;
    GameObject obstacle;

    private void Awake()
    {
        generateTime = UnityEngine.Random.Range(TIMERANGE_MIN, TIMERANGE_MAX);
    }

    // Update is called once per frame
    void Update()
    {
        generateTime -= Time.deltaTime;
        if( generateTime < 0.0f && !isGenerated )
        {
            ObstacleGenerate();
        }

        if( isGenerated )
        {
            lifeTime -= Time.deltaTime;
        }

        if (lifeTime < 0.0f)
        {
            Destroy(obstacle.gameObject);
            Destroy(this.gameObject);
        }
        else if (lifeTime < 0.75f)
        {
            obstacle.GetComponent<FlashComponent>().RunFlash();
        }
    }

    private void ObstacleGenerate()
    {
        Vector3 transform = Vector3.zero;
        transform.y = GENERATE_OFFSET;

        int num = UnityEngine.Random.Range(0, obstacles.Length);
        obstacle = Instantiate(obstacles[num], this.transform);
        obstacle.transform.position = transform;

        isGenerated = true;
    }
}
