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

    private void Awake()
    {
        PlayerInputManager playerInputManager = GetComponent<PlayerInputManager>();

        if (playerInputManager != null)
        {
            playerInputManager.onPlayerJoined += OnPlayerJoined;
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
    }

}
