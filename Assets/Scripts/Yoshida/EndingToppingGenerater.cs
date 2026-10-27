using UnityEngine;

public class EndingToppingGenerater : MonoBehaviour
{
    [SerializeField] private GameObject toppings;

    public void Generate()
    {
        Instantiate(toppings, this.transform);
    }
}
