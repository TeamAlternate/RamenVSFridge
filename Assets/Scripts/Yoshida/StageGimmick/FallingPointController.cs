using System;
using TMPro;
using UnityEngine;
using UnityEngine.LowLevelPhysics;
using UnityEngine.Events;
using Unity.VisualScripting;

public class FallingPointController : MonoBehaviour
{
    const float GIMMICK_OFFSET = 0.2f;
    const float RANGE_MIN = -3.0f;
    const float RANGE_MAX = 3.0f;

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
    public UnityEvent playerHitEvent;


    public void Initialize()
    {
        collider.enabled = false;
        SetGimmickPosition();
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
            UpdateFloatText(timer);
        }
    }

    private void SetGimmickPosition()
    {
        Vector3 newPosition = Vector3.zero;
        newPosition.x = UnityEngine.Random.Range(RANGE_MIN, RANGE_MAX);
        newPosition.y = GIMMICK_OFFSET;
        newPosition.z = UnityEngine.Random.Range(RANGE_MIN, RANGE_MAX);

        this.transform.position = newPosition;
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
            Debug.Log("Player Hited");
        }
    }
}
