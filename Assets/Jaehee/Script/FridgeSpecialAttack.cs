using UnityEngine;

public class FridgeSpecialAttack : MonoBehaviour
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
        if (hasHit || !other.CompareTag("Ramen"))
        {
            return;
        }

        hasHit = true;
        PlayerMovement target = other.GetComponentInParent<PlayerMovement>();
        if (target != null)
        {
            target.ApplyKnockback(transform.root.position, knockbackSpeed, knockbackDuration);
        }

        TimeManager.instance.ChangeTimeState(TimeState.Accele);
    }
}
