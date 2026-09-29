using UnityEngine;

public class RamenAttack : MonoBehaviour
{
    [SerializeField] private float knockbackSpeed = 8f;
    [SerializeField] private float knockbackDuration = 0.25f;
    private bool hasHit;

    private void OnEnable()
    {
        hasHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasHit && other.gameObject.CompareTag("Fridge"))
        {
            hasHit = true;
            PlayerMovement target = other.GetComponentInParent<PlayerMovement>();

            if (target != null)
            {
                target.ApplyKnockback(transform.root.position, knockbackSpeed, knockbackDuration);
            }

            Transform fridgeTransform = other.gameObject.GetComponent<Transform>();
            ToppingManager.instance.SpawnTopping(fridgeTransform.position);

        }
    }
}
