using UnityEngine;

public class ToppingManager : MonoBehaviour
{
    public static ToppingManager instance { get; private set; }

    public GameObject[] toppingObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnTopping(Vector3 spawnPos)
    {
        int randomValue = Random.Range(0, toppingObject.Length);

        GameObject topping = Instantiate(toppingObject[randomValue], spawnPos +
            Vector3.up * 1.7f, Quaternion.identity);

        if (topping.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce
                (new Vector3(Random.Range(-1.2f, 1.2f), 5f,
                Random.Range(-1.2f, 1.2f)), ForceMode.Impulse);

            rb.AddTorque
               (Random.insideUnitSphere * 3f, ForceMode.Impulse);
        }

        Debug.Log("Spawn Topping");
    }
}
