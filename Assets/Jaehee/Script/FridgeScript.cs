using UnityEngine;

public class FridgeScript : MonoBehaviour
{
    private void Awake()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Fridge"),
            LayerMask.NameToLayer("Topping"),true);
    }
}
