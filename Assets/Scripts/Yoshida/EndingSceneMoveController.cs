using Scenes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EndingSceneMoveController : MonoBehaviour
{
    [SerializeField] private FillCube fillCubeController;
    [SerializeField] private bool isMoveToTitle = true;
    private float needStayTime = 3.0f;
    private float stayRemainingTime;
    private bool isStayRamen = false;
    private bool isStayFridge = false;
    private bool isGameStarted = false;

    [SerializeField] private UnityEvent onMoveTitle;
    [SerializeField] private UnityEvent onMoveMainGame;

    private void Awake()
    {
        stayRemainingTime = needStayTime;
        isGameStarted = false;
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
            stayRemainingTime -= Time.deltaTime;
            fillCubeController.FillUpdate(1.0f - (stayRemainingTime / needStayTime));
            if (stayRemainingTime < 0.0f)
            {
                SceneMove();
            }
        }
        else
        {
            stayRemainingTime = needStayTime;
            fillCubeController.FillReset();
        }
    }

    private void SceneMove()
    {
        if ( isMoveToTitle )
        {
            onMoveTitle.Invoke();
        }
        else
        {
            onMoveMainGame.Invoke();
        }
        isGameStarted = true;
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
