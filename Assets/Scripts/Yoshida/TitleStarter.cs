using Scenes;
using UnityEngine;

/// <summary>
/// タイトルのスタート管理クラス
/// </summary>
public class TitleStarter : MonoBehaviour
{
    private GameObject titleManagerGameObject;
    private float startStayTime = 5.0f;
    private float startRemainingTime;
    private bool isStayRamen = false;
    private bool isStayFridge = false;

    private void Awake()
    {
        titleManagerGameObject = GameObject.Find("Manager");
        if( !titleManagerGameObject )
        {
            Debug.LogWarning("TitleManager not Find");
        }

        startRemainingTime = startStayTime;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeRemaining();
    }

    private void TimeRemaining()
    {
        if( isStayRamen && isStayFridge )
        {
            startRemainingTime -= Time.deltaTime;
            StartGame();
        }
        else
        {
            startRemainingTime = startStayTime;
        }

        Debug.Log(startRemainingTime);
    }

    private void StartGame()
    {
        titleManagerGameObject.GetComponent<TitleManager>().StartGame();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isRamen = other.gameObject.tag == "Ramen";
        bool isFridge = other.gameObject.tag == "Fridge";

        if( isRamen )
        {
            isStayRamen = true;
        }
        if( isFridge )
        {
            isStayFridge = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bool isRamen = other.gameObject.tag == "Ramen";
        bool isFridge = other.gameObject.tag == "Fridge";

        if (isRamen)
        {
            isStayRamen = false;
        }
        if (isFridge)
        {
            isStayFridge = false;
        }
    }
}
