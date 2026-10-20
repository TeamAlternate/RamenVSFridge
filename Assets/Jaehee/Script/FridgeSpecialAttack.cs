using UnityEngine;

public class FridgeSpecialAttack : MonoBehaviour
{
    [SerializeField] private float knockbackSpeed = 10f;
    [SerializeField] private float knockbackDuration = 0.4f;
    private const int HitCooldownTime = 15;
    private int hitCT = 0;

    private void OnEnable()
    {
        hitCT = 0;
    }

    private void Update()
    {
        hitCT--;
    }

    private void OnTriggerStay(Collider other)
    {
        if (hitCT > 0 || !other.CompareTag("Ramen"))
        {
            return;
        }

        // hasHit = true;
        hitCT = HitCooldownTime;
        PlayerMovement target = other.GetComponentInParent<PlayerMovement>();
        if (target != null)
        {
            target.ApplyKnockback(transform.root.position, knockbackSpeed, knockbackDuration);
        }

        TimeManager.instance.ChangeTimeState(TimeState.Accele);
    }
}
