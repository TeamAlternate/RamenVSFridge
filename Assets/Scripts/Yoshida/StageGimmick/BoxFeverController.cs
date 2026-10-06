using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

public class BoxFeverController : MonoBehaviour
{
    [SerializeField] private GameObject[] boxPrefab;
    [SerializeField] private int generateQuantity = 20;
    [SerializeField] private float lifeTime = 10.0f;
    [SerializeField] private PhysicsMaterial physics;

    float boxOffset = 10.0f;
    float generateInterval = 0.2f;
    float intervalTime = 0.0f;
    int generateCount = 0;
    float flashInterval = 0.1f;
    float flashIntervalTime = 0.0f;

    List<GameObject> boxes = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        intervalTime = generateInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (generateCount < generateQuantity)
        {
            intervalTime -= Time.deltaTime;
            if (intervalTime < 0.0f)
            {
                GenerateBox();
            }
        }
        else
        {
            lifeTime -= Time.deltaTime;
            if( lifeTime < 0.0f )
            {
                foreach( GameObject box in boxes )
                {
                    Destroy(box.gameObject);
                }
                Destroy(this.gameObject);
            }
            else if( lifeTime < 0.75f )
            {
                if (flashIntervalTime < 0.0f)
                {
                    foreach (GameObject box in boxes)
                    {
                        if (box.GetComponent<MeshRenderer>().enabled == false)
                        {
                            box.GetComponent<MeshRenderer>().enabled = true;
                        }
                        else
                        {
                            box.GetComponent<MeshRenderer>().enabled = false;
                        }
                    }
                    flashIntervalTime = flashInterval;
                }
                else
                {
                    flashIntervalTime -= Time.deltaTime;
                }
            }
        }
    }

    private void GenerateBox()
    {
        // transform ê›íË
        Vector3 boxPosition = this.transform.position;
        boxPosition.y += boxOffset;
        Quaternion boxRotation = Quaternion.identity;
        boxRotation.x = UnityEngine.Random.Range(0f, 360f);
        boxRotation.y = UnityEngine.Random.Range(0f, 360f);
        boxRotation.z = UnityEngine.Random.Range(0f, 360f);
        Vector3 scale = new Vector3(2.5f, 2.5f, 2.5f);

        // boxÇÃèâä˙âª
        int num = UnityEngine.Random.Range(0, boxPrefab.Length);
        GameObject generateBox = Instantiate(boxPrefab[num], this.transform);
        generateBox.transform.position = boxPosition;
        generateBox.transform.rotation = boxRotation;
        generateBox.transform.localScale = scale;
        generateBox.AddComponent<Rigidbody>();
        generateBox.AddComponent<CapsuleCollider>();
        generateBox.GetComponent<Rigidbody>().mass = 0.125f;
        generateBox.GetComponent<CapsuleCollider>().material = physics;
        boxes.Add(generateBox);

        generateCount++;
        intervalTime = generateInterval;
    }
}
