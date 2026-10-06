using UnityEngine;

public class StageGimmickGenerater : MonoBehaviour
{
    [SerializeField] private FallingPointController fallingGimmick;
    [SerializeField] private bool isEnableFalling = true;
    [SerializeField] private float gimmickIntervalMin = 5.0f;
    [SerializeField] private float gimmickIntervalMax = 15.0f;

    float intervalTime = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        intervalTime = UnityEngine.Random.Range(gimmickIntervalMin, gimmickIntervalMax);
    }

    // Update is called once per frame
    void Update()
    {
        if( intervalTime < 0.0f )
        {
            FallingPointController falling = Instantiate(fallingGimmick, this.transform);
            falling.Initialize();

            intervalTime = UnityEngine.Random.Range(gimmickIntervalMin, gimmickIntervalMax);
        }

        intervalTime -= Time.deltaTime;        
    }
}
