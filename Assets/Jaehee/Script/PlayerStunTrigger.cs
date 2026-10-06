using UnityEngine;

public class PlayerStunTrigger : MonoBehaviour
{
    [SerializeField] private float stunDuration = 2.0f;
    private bool isStun;

    private void Awake()
    {
        isStun = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponentInParent<PlayerMovement>();
        if (player != null)
        {
            if(isStun)
            {
                return; 
            }

            Debug.Log("ƒXƒ^ƒ“");
            isStun = true;
            player.Stun(stunDuration);
        }
    }
}
