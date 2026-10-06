using UnityEngine;

public class FridgeAttack : MonoBehaviour
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
        if (!hasHit && other.gameObject.CompareTag("Ramen"))
        {
            hasHit = true;
            PlayerMovement target = other.GetComponentInParent<PlayerMovement>();

            if (target != null)
            {
                target.ApplyKnockback(transform.root.position, knockbackSpeed, knockbackDuration);
            }

            Debug.Log("—â‚â‚·");
            TimeManager.instance.ChangeTimeState(TimeState.Accele);
        }
    }
}
