using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class CloudMove : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Image down;
    [SerializeField] private Image up;
    
    float moveAmount = -440.0f;
    Vector3 startPosition = Vector3.zero;

    private void Awake()
    {
        startPosition.x = -775.0f + 960.0f;
        startPosition.y = 400.0f + 540.0f;
        VisibleChange(false, false, false);
    }

    public void PositionUpdate(float percent)
    {
        Vector3 newPosition = startPosition;
        newPosition.y += moveAmount * ( 1.0f - percent );

        image.rectTransform.position = newPosition;

        Debug.Log($"percent : {percent}");
    }

    public void VisibleChange(bool cloud, bool down, bool up)
    {
        image.enabled = cloud;
        this.down.enabled = down;
        this.up.enabled = up;
    }
}
