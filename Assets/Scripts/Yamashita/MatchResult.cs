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
    private ResultTypes resultType;

    public static void Update(MatchResult newResult)
    {
        recent = newResult;
    }

}
