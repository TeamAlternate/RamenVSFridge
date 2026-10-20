using UnityEngine;

public class Topping : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ramen"))
        {
            ScoreManager.instance.AddToppintScore();
            GameObject go = Instantiate(this.gameObject);
            UserInterfaces.ToppingDisplay.AddTopping(go);
            Debug.Log("Create", go);
            Destroy(gameObject);
        }
    }
}
