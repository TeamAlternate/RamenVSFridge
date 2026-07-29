using UnityEngine;

public class MatchResult 
{
    private static MatchResult recent;

    private MatchSettings settings;

    public enum ResultTypes
    {
        None,
        RamenWin,
        FridgeWin,
        Draw,
    }
    public ResultTypes resultType;
    // public Topping[] collectedToppings;


    public static void Update(MatchResult newResult)
    {
        recent = newResult;
    }



}
