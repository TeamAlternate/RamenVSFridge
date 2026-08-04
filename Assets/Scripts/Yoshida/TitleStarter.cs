using Scenes;
using UnityEngine;

/// <summary>
/// タイトルのスタート管理クラス
/// </summary>
public class TitleStarter : MonoBehaviour
{
    [SerializeField] private FillCube fillCubeController;
    private GameObject titleManagerGameObject;
    private float startStayTime = 5.0f;
    private float startRemainingTime;
    private bool isStayRamen = false;
    private bool isStayFridge = false;
    private bool isGameStarted = false;

    private void Awake()
    {
        titleManagerGameObject = GameObject.Find("Manager");
        if( !titleManagerGameObject )
        {
            Debug.LogWarning("TitleManager not Find");
        }

        startRemainingTime = startStayTime;
        isGameStarted = false;
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
            fillCubeController.FillUpdate(1.0f - (startRemainingTime / startStayTime));
            if (startRemainingTime < 0.0f)
            {
                StartGame();
            }
        }
        else
        {
            startRemainingTime = startStayTime;
            fillCubeController.FillReset();
        }

        Debug.Log(startRemainingTime);
    }

    private void StartGame()
    {
        if (!isGameStarted)
        {
            titleManagerGameObject.GetComponent<TitleManager>().StartGame();
            isGameStarted = true;
        }
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
