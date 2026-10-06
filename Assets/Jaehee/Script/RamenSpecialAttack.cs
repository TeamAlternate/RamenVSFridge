using UnityEngine;

public class RamenSpecialAttack : MonoBehaviour
{
    [SerializeField] private float knockbackSpeed = 10f;
    [SerializeField] private float knockbackDuration = 0.4f;
    private bool hasHit;

    private void OnEnable()
    {
        hasHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit || !other.CompareTag("Fridge"))
        {
            return;
        }

        hasHit = true;
        PlayerMovement target = other.GetComponentInParent<PlayerMovement>();
        if (target != null)
        {
            target.ApplyKnockback(transform.root.position, knockbackSpeed, knockbackDuration);
        }

        ToppingManager.instance.SpawnTopping(other.transform.position);
    }
}
