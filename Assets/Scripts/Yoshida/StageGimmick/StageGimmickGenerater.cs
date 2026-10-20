using UnityEngine;

public struct GimmickSetting
{
    public GimmickSetting( float v1, float v2 )
    {
        this.gimmickIntervalMin = v1;
        this.gimmickIntervalMax = v2;
        this.intervalTime = 0.0f;
    }

    public float gimmickIntervalMin;
    public float gimmickIntervalMax;
    public float intervalTime;
}

public class StageGimmickGenerater : MonoBehaviour
{
    [SerializeField] private FallingPointController fallingGimmick;
    [SerializeField] bool isEnableFalling = true;

    [SerializeField] private BoxFeverController boxFeverGimmick;
    [SerializeField] bool isEnableBoxFever = true;

    GimmickSetting fallingSetting = new GimmickSetting( 1.5f, 7.0f );
    GimmickSetting boxFeverSetting = new GimmickSetting( 15.0f, 30.0f );

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fallingSetting.intervalTime = UnityEngine.Random.Range(fallingSetting.gimmickIntervalMin, fallingSetting.gimmickIntervalMax);
        boxFeverSetting.intervalTime = UnityEngine.Random.Range(boxFeverSetting.gimmickIntervalMin, boxFeverSetting.gimmickIntervalMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnableFalling)
        {
            UpdateFalling();
        }

        if (isEnableBoxFever)
        {
            UpdateBoxFever();
        }
    }

    private void UpdateFalling()
    {
        if (fallingSetting.intervalTime < 0.0f)
        {
            FallingPointController falling = Instantiate(fallingGimmick, this.transform);
            falling.Initialize();

            fallingSetting.intervalTime = UnityEngine.Random.Range(fallingSetting.gimmickIntervalMin, fallingSetting.gimmickIntervalMax);
        }

        fallingSetting.intervalTime -= Time.deltaTime;
    }

    private void UpdateBoxFever()
    {
        if (boxFeverSetting.intervalTime < 0.0f)
        {
            BoxFeverController fever = Instantiate(boxFeverGimmick, this.transform);

            boxFeverSetting.intervalTime = UnityEngine.Random.Range(boxFeverSetting.gimmickIntervalMin, boxFeverSetting.gimmickIntervalMax);
        }

        boxFeverSetting.intervalTime -= Time.deltaTime;
    }
}
