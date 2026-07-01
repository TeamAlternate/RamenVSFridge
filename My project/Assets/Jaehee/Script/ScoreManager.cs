using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }

    private int toppingScore = 0;
    private const int maxToppintScore = 4;

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
        Debug.Log("ToppingScore:" + toppingScore);
    }

    private void RamenWin()
    {
        if (toppingScore < maxToppintScore)
        {
            return;
        }
        //Debug.Log("RamenWin");
    }
}
