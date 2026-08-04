using UnityEngine;

public class CampfireController : MonoBehaviour
{
    private float lifeTime = 10.0f;
    private float accelerate = 1.5f;
    private Vector3 transformRangeMin = new Vector3(-4.0f, 0.0f, -4.0f);
    private Vector3 transformRangeMax = new Vector3(4.0f, 0.0f, 4.0f);

    private void Awake()
    {
        this.transform.position = GetNewTransform();
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if( lifeTime <= 0.0f )
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        bool isRamen = other.gameObject.tag == "Ramen";
        if (isRamen)
        {
            // 自身のライフタイムの減少を加速する
            lifeTime -= Time.deltaTime * (accelerate - 1.0f);
        }
    }

    private Vector3 GetNewTransform()
    {
        Vector3 newTransfrom = Vector3.zero;
        newTransfrom.x = Random.Range(transformRangeMin.x, transformRangeMax.x);
        newTransfrom.z = Random.Range(transformRangeMin.z, transformRangeMax.z);

        return newTransfrom;
    }
}
