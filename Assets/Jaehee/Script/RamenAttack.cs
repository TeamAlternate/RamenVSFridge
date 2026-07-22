using UnityEngine;

public class RamenAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fridge"))
        {
            Transform fridgeTransform = other.gameObject.GetComponent<Transform>();
            ToppingManager.instance.SpawnTopping(fridgeTransform.position);
        }
    }
}
