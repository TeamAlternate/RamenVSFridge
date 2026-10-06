using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] private float jumpPower = 5f;

    [Header("etc")]
    private bool isGround;
    private readonly HashSet<Collider> groundColliders = new HashSet<Collider>();
    private float stunEndTime;
    public bool IsStunned => Time.time < stunEndTime;

    private Vector2 moveInput;
    private Rigidbody rb;
    private Vector3 knockbackVelocity;
    private float knockbackTimeRemaining;

    private int playerIndex;
    private GameObject currentCharacter;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isGround = false;

        CameraController.AddTarget(gameObject);
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
        if (input.isPressed && isGround && !IsStunned)
        {
            rb.linearVelocity = new Vector3(
                rb.linearVelocity.x,
                jumpPower,
                rb.linearVelocity.z
            );
        }
    }

    public void ApplyKnockback(Vector3 attackerPosition, float speed, float duration)
    {
        if (speed <= 0f || duration <= 0f)
        {
            return;
        }

        Vector3 direction = transform.position - attackerPosition;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.0001f)
        {
            direction = -transform.forward;
        }

        knockbackVelocity = direction.normalized * speed;
        knockbackTimeRemaining = duration;
    }

    public void Stun(float duration)
    {
        if (duration <= 0f)
        {
            return;
        }

        stunEndTime = Mathf.Max(stunEndTime, Time.time + duration);
        knockbackTimeRemaining = 0f;
        knockbackVelocity = Vector3.zero;
        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);

        if (currentCharacter != null)
        {
            currentCharacter.GetComponent<RamenScript>()?.CancelAttacks();
            currentCharacter.GetComponent<FridgeScript>()?.CancelAttacks();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (IsStunned)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            return;
        }

        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        // ナックバック
        bool isKnockedBack = knockbackTimeRemaining > 0f;
        Vector3 velocity;
        if (isKnockedBack)
        {
            velocity = knockbackVelocity;
            knockbackTimeRemaining -= Time.fixedDeltaTime;
        }
        else
        {
            velocity = move * moveSpeed;
        }
        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;

        if (!isKnockedBack && move.sqrMagnitude > 0.01f && currentCharacter != null)
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
            groundColliders.Add(collision.collider);
            isGround = groundColliders.Count > 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            groundColliders.Remove(collision.collider);
            isGround = groundColliders.Count > 0;
        }
    }

}
