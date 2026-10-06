using UnityEngine;

public class StageScaler : MonoBehaviour
{
    [SerializeField] float scalingSpeed = 0.005f;

    private void Update()
    {
        AddScale();
    }

    private void AddScale()
    {
        Vector3 newScale = transform.localScale;
        newScale.x += scalingSpeed * Time.deltaTime;
        newScale.y += scalingSpeed * Time.deltaTime;
        newScale.z += scalingSpeed * Time.deltaTime;

        this.transform.localScale = newScale;
    }
}
