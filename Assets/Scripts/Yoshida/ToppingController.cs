using UnityEngine;

public enum ToppingType
{
    Chashu,
    Egg,
    Moyashi,
    Noodle
};

public class ToppingController : MonoBehaviour
{
    [SerializeField] ToppingType toppringType;

    float moveY = 0.5f;
    float time = 0.0f;
    float rotateSpeed = 1.0f;

    private void Update()
    {
        time += Time.deltaTime;

        UpdateTransform();
        UpdateRotate();
    }

    private void UpdateTransform()
    {
        Vector3 update = transform.localPosition;
        update.y = moveY * Mathf.Sin(time);
        transform.localPosition = update;
    }

    private void UpdateRotate()
    {
        Vector3 add = Vector3.zero;
        add.y = rotateSpeed;
        transform.Rotate(add);
    }
}
