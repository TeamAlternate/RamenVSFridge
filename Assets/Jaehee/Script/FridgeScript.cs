using UnityEngine;

public class FridgeScript : MonoBehaviour
{
    BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = transform.parent.GetComponent<BoxCollider>();
        boxCollider.center = new Vector3(0.0f, -0.5f, 0.0f);
    }
}
