using UnityEngine;
using UnityEngine.InputSystem;

public class FridgeScript : MonoBehaviour
{
    BoxCollider boxCollider;

    [SerializeField]
    GameObject attackCollider;

    private const float maxAttackTime = 2.0f;
    private float attackTime = 0.0f;
    private bool attackChecker = false;

    private void Awake()
    {
        // ground collider pivot
        boxCollider = transform.parent.GetComponent<BoxCollider>();
        boxCollider.center = new Vector3(0.0f, -0.5f, 0.0f);

        attackCollider = transform.GetChild(0).gameObject;
        if (attackCollider)
        {
            attackCollider.SetActive(false);
        }
    }

    private void Update()
    {
        AttackTimer();
    }

    public void OnAttack(InputValue input)
    {
        if (input.isPressed && !attackChecker)
        {
            Debug.Log("Fridge attack");
            attackTime = 0.0f;
            attackChecker = true;
            attackCollider.SetActive(true);
        }
    }

    private void AttackTimer()
    {
        if (!attackChecker)
        {
            return;
        }

        attackTime += Time.deltaTime;

        if (attackTime >= maxAttackTime)
        {
            attackChecker = false;
            attackCollider.SetActive(false);
        }
    }
}
