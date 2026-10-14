using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinManager : MonoBehaviour
{
    // array setting
    // 1 - ramen
    // 2- fridge
    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Character Prefabs")]
    [SerializeField] private GameObject[] characterPrefabs;

    public PlayerInputData[] playerDevice { get; private set; } = new PlayerInputData[2]; 

    private void Awake()
    {
        PlayerInputManager playerInputManager = GetComponent<PlayerInputManager>();

        if (playerInputManager != null)
        {
            playerInputManager.onPlayerJoined += OnPlayerJoined;
        }
    }

    private void Start()
    {
        if (MatchSettings.recent.ramenPlayer.device != null && MatchSettings.recent.fridgePlayer.device != null)
        {
            SpawnPlayer(MatchSettings.recent.ramenPlayer);
            SpawnPlayer(MatchSettings.recent.fridgePlayer);
        }
    }

    private void OnDestroy()
    {
        PlayerInputManager playerInputManager = GetComponent<PlayerInputManager>();

        if (playerInputManager != null)
        {
            playerInputManager.onPlayerJoined -= OnPlayerJoined;
        }
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        int index = playerInput.playerIndex;
        playerDevice[index].device = playerInput.GetDevice<InputDevice>();
        playerDevice[index].userIndex = playerInput.user.index;

        Debug.Log($"Player {index + 1} Joined");

        if (index < spawnPoints.Length)
        {
            playerInput.transform.position = spawnPoints[index].position;
        }

        PlayerMovement movement = playerInput.GetComponent<PlayerMovement>();

        if (movement != null)
        {
            movement.SetPlayerIndex(index);

            if (index < characterPrefabs.Length)
            {
                movement.SetCharacter(characterPrefabs[index]);
            }
        }

        if( index + 1 == 2 )
        {
            GameObject time = GameObject.Find("TimeManager");
            if (time != null)
            {
                TimeManager timeManager = time.GetComponent<TimeManager>();
                if (timeManager != null)
                {
                    timeManager.StartTimer();
                }
            }
        }
    }

    private void SpawnPlayer(PlayerInputData playerData)
    {
        PlayerInputManager playerInputManager = this.GetComponent<PlayerInputManager>();
        PlayerInput playerInput = PlayerInput.Instantiate(playerInputManager.playerPrefab, playerIndex: -1, pairWithDevice: playerData.device);

        int index = playerInput.playerIndex;

        if( index < spawnPoints.Length )
        {
            playerInput.transform.position = spawnPoints[index].position;
        }

        PlayerMovement movement = playerInput.GetComponent<PlayerMovement>();

        if (movement != null)
        {
            movement.SetPlayerIndex(index);

            if (index < characterPrefabs.Length)
            {
                movement.SetCharacter(characterPrefabs[index]);
            }
        }
    }
}
