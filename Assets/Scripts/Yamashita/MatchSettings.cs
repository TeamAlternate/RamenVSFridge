using UnityEngine;
using UnityEngine.InputSystem;

public struct PlayerInputData
{ 
    public InputDevice device;
    public int userIndex;
}

public class MatchSettings
{
    public static MatchSettings recent = new MatchSettings();

    private float initialTimeLimit;

    public PlayerInputData ramenPlayer, fridgePlayer;

    public static void Update(MatchSettings newSettings)
    {
        recent = newSettings;
    }
}

