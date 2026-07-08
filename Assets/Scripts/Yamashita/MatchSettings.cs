using UnityEngine;

public class MatchSettings
{
    public static MatchSettings recent;



    public static void Update(MatchSettings newSettings)
    {
        recent = newSettings;
    }
}

