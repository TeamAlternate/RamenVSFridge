using System.Collections.Generic;
using UnityEngine;

namespace _Tester
{
    public class CameraTest : MonoBehaviour
    {
        [SerializeField] private List<GameObject> targets;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            foreach (GameObject target in targets)
            {
                CameraController.AddTarget(target);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
