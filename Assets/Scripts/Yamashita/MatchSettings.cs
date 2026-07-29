using UnityEngine;

public class MatchSettings
{
    public static MatchSettings recent;

    private float initialTimeLimit;

    public static void Update(MatchSettings newSettings)
    {
        recent = newSettings;
    }
}

