using System;
using TMPro;
using UnityEngine;
using UnityEngine.LowLevelPhysics;

public class FallingPointController : MonoBehaviour
{
    [SerializeField] private float timer = 5.0f;
    [SerializeField] private float radius = 1.5f;
    [SerializeField] private TextMeshPro text;
    [SerializeField] private SpriteRenderer circle;
    [SerializeField] private SphereCollider collider;
    [SerializeField] private GameObject box;

    float boxOffset = 10.0f;
    bool isGenerate = false;
    GameObject generateBox;

    /// <summary>
    /// プレイヤー衝突時のメソッド呼び出し
    /// </summary>
    public Action playerHitEvent;

    private void Awake()
    {
        collider.enabled = false;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if( timer <= 0.0f )
        {
            if( timer < -0.75f && generateBox != null)
            {
                Destroy(generateBox.gameObject);
                generateBox = null;
                Destroy(this.gameObject);
            }
            text.text = "";
            collider.enabled = true;
        }
        else if( timer < 1.0f)
        {
            UpdateFloatText(timer);
        }
        else if (!isGenerate && timer < 1.25f)
        {
            isGenerate = true;
            GenerateBox();
        }
        else
        {
            UpdateIntText((int)timer);
        }
    }

    private void GenerateBox()
    {
        Vector3 boxPosition = this.transform.position;
        boxPosition.y += boxOffset;
        Quaternion boxRotation = Quaternion.identity;
        boxRotation.x = UnityEngine.Random.Range(0f, 360f);
        boxRotation.y = UnityEngine.Random.Range(0f, 360f);
        boxRotation.z = UnityEngine.Random.Range(0f, 360f);
        Vector3 scale = Vector3.one;
        scale += Vector3.one;

        generateBox = Instantiate(box, this.transform);
        generateBox.transform.position = boxPosition;
        generateBox.transform.rotation = boxRotation;
        generateBox.transform.localScale = scale;
        generateBox.AddComponent<Rigidbody>();
        generateBox.AddComponent<CapsuleCollider>();
    }

    private void UpdateIntText(int time)
    {
        text.text = time.ToString();
    }

    private void UpdateFloatText(float time)
    {
        text.text = string.Format("{0:F1}", time);
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isRamen = other.gameObject.tag == "Ramen";
        bool isFridge = other.gameObject.tag == "Fridge";

        if (isRamen || isFridge)
        {
            playerHitEvent.Invoke();
        }
    }
}
