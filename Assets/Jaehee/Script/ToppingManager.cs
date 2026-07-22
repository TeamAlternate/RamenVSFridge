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

        Debug.Log("Spawn Topping");
        Instantiate(toppingObject[randomValue], spawnPos, Quaternion.identity);
    }
}
