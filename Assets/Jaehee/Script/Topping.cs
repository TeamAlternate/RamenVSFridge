using UnityEngine;

public class Topping : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ramen"))
        {
            ScoreManager.instance.AddToppintScore();
            Destroy(gameObject);
        }
    }
}
