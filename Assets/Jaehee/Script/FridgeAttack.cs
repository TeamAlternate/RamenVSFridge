using UnityEngine;

public class FridgeAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ramen"))
        {
            Debug.Log("—â‚â‚·");
            TimeManager.instance.ChangeTimeState(TimeState.Accele);
        }
    }
}
