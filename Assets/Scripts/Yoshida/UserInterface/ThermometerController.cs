using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

/// <summary>
/// Žc‚èŽžŠÔ•\Ž¦
/// </summary>
public class ThermometerController : MonoBehaviour
{
    [SerializeField] private Image thermometerImage;
    [SerializeField] private Image liquidImage;
    [SerializeField] private CloudMove cloudImage;

    private TimeManager timeManagerGameObject;
    private CloudMove cloudMoveGameObject;
    private float remainingsTime = 0.0f;
    private float timeMax;

    public void Initialize(TimeManager timeManager)
    {
        timeManagerGameObject = timeManager;
        this.remainingsTime = timeManagerGameObject.time;
        this.timeMax = timeManagerGameObject.timeMax;

        cloudMoveGameObject = Instantiate(cloudImage, this.transform);
    }

    private void Update()
    {
        this.remainingsTime = timeManagerGameObject.time;

        float percent = remainingsTime / timeMax;
        ImageUpdate(percent);
        ColorUpdate(timeManagerGameObject.GetTimeState());
        cloudMoveGameObject.PositionUpdate(percent);
    }

    private void ImageUpdate(float percent)
    {
        thermometerImage.fillAmount = percent;
    }

    private void ColorUpdate(TimeState state)
    {
        Color newTextColor = Color.white;

        switch (state)
        {
            case TimeState.Normal:
                cloudMoveGameObject.VisibleChange(false, false, false);
                break;
            case TimeState.Accele:
                cloudMoveGameObject.VisibleChange(true, true, false);
                newTextColor = Color.cyan;
                break;
            case TimeState.Decele:
                cloudMoveGameObject.VisibleChange(true, false, true);
                newTextColor = Color.red;
                break;
        }

        thermometerImage.color = newTextColor;
        liquidImage.color = newTextColor;
    }
}
