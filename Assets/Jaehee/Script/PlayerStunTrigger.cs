using UnityEngine;

public class PlayerStunTrigger : MonoBehaviour
{
    [SerializeField] private float stunDuration = 2.0f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponentInParent<PlayerMovement>();
        if (player != null)
        {
            player.Stun(stunDuration);
        }
    }
}
