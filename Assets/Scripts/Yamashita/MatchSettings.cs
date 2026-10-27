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

    public float lastTime { get; private set; } = 0.0f;
    public int getToppings { get; private set; } = 0;

    public void MatchSetting()
    {
        lastTime = 0.0f;
        getToppings = 0;
    }

    public static void Update(MatchSettings newSettings)
    {
        recent = newSettings;
    }

    public void SetResult(float time, int toppings)
    {
        lastTime = time;
        getToppings = toppings; 
    }
}

