using UnityEngine;

public enum TimeState
{
    Normal,
    Accele,
    Decele
};

/// <summary>
/// インゲーム時間管理
/// </summary>
public class TimeManager : MonoBehaviour
{
    [SerializeField] private float timeMax = 180.0f;

    private float time = 180.0f;
    private float accelerate = 1.5f;
    private float decelerate = 0.75f;
    private TimeState state = TimeState.Normal;

    private KeyCode DebugAccele = KeyCode.F1;
    private KeyCode DebugDecele = KeyCode.F2;


    /// <summary>
    /// 起動時処理
    /// </summary>
    private void Awake()
    {
        Initialize();

        // デバッグ用
        Application.targetFrameRate = 60;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
        time = timeMax;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        ChangeTimeState(TimeState.Normal);
        if( Input.GetKey(DebugAccele))
        {
            ChangeTimeState(TimeState.Accele);
        }
        if( Input.GetKey(DebugDecele))
        {
            ChangeTimeState(TimeState.Decele);
        }

        TimeDecrease();
    }

    /// <summary>
    /// 状態更新
    /// </summary>
    /// <param name="newState">
    /// 次の状態
    /// </param>
    public void ChangeTimeState(TimeState newState)
    {
        state = newState;
    }

    /// <summary>
    /// 残り時間減少処理
    /// </summary>
    private void TimeDecrease()
    {
        float decrease = Time.deltaTime;

        switch(state)
        {
            case TimeState.Normal:
            default:
                break;
            case TimeState.Accele:
                decrease *= accelerate;
                break;
            case TimeState.Decele:
                decrease *= decelerate;
                break;
        }

        time -= decrease;
    }
}
