using UnityEngine;

public class MatchResult 
{
    private static MatchResult recent;

    public MatchSettings settings;

    public enum ResultTypes
    {
        None,
        RamenWin,
        FridgeWin,
        Draw,
    }
    public ResultTypes resultType;
    // public Topping[] collectedToppings;

    public static MatchResult GetRecent()
    {
        
        return recent ?? new MatchResult() { settings = new MatchSettings(), resultType = ResultTypes.None };
    }

    public static void Update(MatchResult newResult)
    {
        recent = newResult;
    }



}
