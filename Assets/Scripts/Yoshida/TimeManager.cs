using UnityEngine;
using System;
using Unity.VisualScripting;

/// <summary>
/// 時間の進行速度ステート
/// </summary>
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

    public static TimeManager instance { get; private set; }

    [SerializeField] public float timeMax = 180.0f;
    [SerializeField] private ThermometerController thermometerControllerGameObject;
    [SerializeField] private GameObject finishAnimationGameObject;

    public float time { get; private set; }
    private float accelerate = 5.0f;
    private float decelerate = -2.5f;
    private TimeState state = TimeState.Normal;
    private bool isTimeup = false;
    private bool isStarted = false;

    // 残り時間の減少加速中に減る最大秒数
    [SerializeField] private float accelerateElapsedMax = 25.0f;

    // 残り時間の減少加速中に減った秒数
    private float accelerateElapsedTime = 0.0f;

    // 時間切れイベント
    public event Action OnTimeup;

    /// <summary>
    /// 起動時処理
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Initialize();

        var timeRemainingsDisplay = Instantiate(thermometerControllerGameObject, this.transform);
        timeRemainingsDisplay.Initialize(this);

        OnTimeup += PlayFinishAnimation;

        // デバッグ用
        Application.targetFrameRate = 60;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
        time = timeMax;
        isTimeup = false;
    }

    /// <summary>
    /// タイマースタート
    /// </summary>
    public void StartTimer()
    {
        isStarted = true;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        if( !isStarted )
        {
            return;
        }

        isTimeup = IsTimeupCheck();

        // デバッグ用(本来であれば時間ステートはラーメン側から行われる)
        //{
        //    ChangeTimeState(TimeState.Normal);
        //    if (Input.GetKey(DebugAccele))
        //    {
        //        ChangeTimeState(TimeState.Accele);
        //    }
        //    if (Input.GetKey(DebugDecele))
        //    {
        //        ChangeTimeState(TimeState.Decele);
        //    }
        //}

        if (!isTimeup)
        {
            TimeDecrease();
        }
    }

    /// <summary>
    /// 終了時処理
    /// </summary>
    private void OnDestroy()
    {
        OnTimeup -= PlayFinishAnimation;
    }

    /// <summary>
    /// 時間切れチェック(イベント発火元)
    /// </summary>
    /// <returns>
    /// 時間切れかどうか
    /// </returns>
    private bool IsTimeupCheck()
    {
        bool onTimeup = time < 0.0f;
        if( onTimeup )
        {
            var timeEvent = OnTimeup;
            if (timeEvent != null && isTimeup == false)
            {
                // イベント発火
                OnTimeup();
            }
        }

        return onTimeup;
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
                if( accelerateElapsedTime > accelerateElapsedMax )
                {
                    ChangeTimeState(TimeState.Normal);
                    accelerateElapsedTime = 0.0f;
                }
                else
                {
                    accelerateElapsedTime += decrease;
                }
                break;
            case TimeState.Decele:
                decrease *= decelerate;
                break;
        }

        time -= decrease;
    }

    /// <summary>
    /// ステートを渡す
    /// </summary>
    /// <returns>今のステート</returns>
    public TimeState GetTimeState()
    {
        return state;
    }

    /// <summary>
    /// 終了時アニメーション
    /// </summary>
    private void PlayFinishAnimation()
    {
        // Instantiate(finishAnimationGameObject, this.transform);
    }
}
