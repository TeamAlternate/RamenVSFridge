using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

/// <summary>
/// Žc‚èŽžŠÔ•\Ž¦
/// </summary>
public class TimeRemainingsDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;

    private TimeManager timeManagerGameObject;
    private float remainingsTime = 0.0f;
    

    public void Initialize(TimeManager timeManager)
    {
        timeManagerGameObject = timeManager;
        this.remainingsTime = timeManagerGameObject.time;
    }

    private void Update()
    {
        this.remainingsTime = timeManagerGameObject.time;
        
        TextUpdate(remainingsTime);
        ColorUpdate(timeManagerGameObject.GetTimeState());
    }

    private void TextUpdate(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;

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
