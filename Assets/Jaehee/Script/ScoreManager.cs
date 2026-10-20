using System;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public UnityEvent GetTopping;

    public static ScoreManager instance { get; private set; }

    [SerializeField]
    private int toppingScore = 0;
    private const int maxToppintScore = 20;

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

    private void Update()
    {
        RamenWin();
    }

    public void AddToppintScore()
    {
        toppingScore++;
        GetTopping.Invoke();
        Debug.Log("ToppingScore:" + toppingScore);
    }

    public int GetScore()
    {
        return toppingScore;
    }

    private void RamenWin()
    {
        if (toppingScore < maxToppintScore)
        {
            return;
        }

        Scenes.MainGameManager.FinishGame(new MatchResult() { resultType = MatchResult.ResultTypes.RamenWin });
    }
}
