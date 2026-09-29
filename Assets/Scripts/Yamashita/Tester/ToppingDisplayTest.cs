using System.Collections;
using UnityEngine;

namespace _Tester
{
    public class ToppingDisplayTest : MonoBehaviour
    {
        [SerializeField] private GameObject[] toppings;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            IEnumerator InternalRoutine()
            {
                foreach (GameObject go in toppings)
                {
                    yield return new WaitForSeconds(1.0f);
                    UserInterfaces.ToppingDisplay.AddTopping(Instantiate(go));
                }
            }
            StartCoroutine(InternalRoutine());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}