using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] private float jumpPower = 5f;

    [Header("etc")]
    private bool isGround;

    private Vector2 moveInput;
    private Rigidbody rb;

    private int playerIndex;
    private GameObject currentCharacter;

  

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isGround = false;
    }

    public void SetPlayerIndex(int index)
    {
        playerIndex = index;
        gameObject.name = $"Player_{playerIndex + 1}";
    }

    public void SetCharacter(GameObject characterPrefab)
    {
        if (characterPrefab == null)
        {
            return;
        }

        if (currentCharacter != null)
        {
            Destroy(currentCharacter);
        }

        currentCharacter = Instantiate(characterPrefab, transform);
        currentCharacter.transform.localPosition = Vector3.zero;
        currentCharacter.transform.localRotation = Quaternion.identity;
    }

    public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

    public void OnJump(InputValue input)
    {
        if (input.isPressed && isGround)
        {
            rb.linearVelocity = new Vector3(
                rb.linearVelocity.x,
                jumpPower,
                rb.linearVelocity.z
            );
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        Vector3 velocity = move * moveSpeed;
        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;

        if (move.sqrMagnitude > 0.01f && currentCharacter != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move.normalized, Vector3.up);

            currentCharacter.transform.rotation =
                Quaternion.RotateTowards( currentCharacter.transform.rotation,
                    targetRotation,  300.0f * Time.fixedDeltaTime );
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

}
