using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.TextCore.Text;

/// <summary>
/// Žc‚èŽžŠÔ•\Ž¦
/// </summary>
public class TimeRemainingsDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TMP_FontAsset fontAsset;

    private TimeManager timeManagerGameObject;
    private float remainingsTime = 0.0f;
    

    public void Initialize(TimeManager timeManager)
    {
        timeManagerGameObject = timeManager;
        this.remainingsTime = timeManagerGameObject.time;
        timeText.font = fontAsset;
    }

    private void Update()
    {
        this.remainingsTime = timeManagerGameObject.time;
        
        TextUpdate(remainingsTime);
        ColorUpdate(timeManagerGameObject.GetTimeState());
    }

    private void TextUpdate(float time)
    {
        int add = 0;
        if( time > 0.0f )
        {
            add = 1;
        }

        int minutes = ((int)time + add) / 60;
        int seconds = ((int)time + add) % 60;

        string newText = $"{minutes:D2}";
        newText += ":";
        newText += $"{seconds:D2}";

        timeText.text = newText;
    }

    private void ColorUpdate(TimeState state)
    {
        Color newTextColor = Color.white;

        switch (state)
        {
            case TimeState.Normal:
                break;
            case TimeState.Accele:
                newTextColor = Color.cyan;
                break;
            case TimeState.Decele:
                newTextColor = Color.red;
                break;
        }

        timeText.color = newTextColor;
    }
}
