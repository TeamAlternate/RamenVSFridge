using UnityEngine;
using UnityEngine.InputSystem;

public class RamenScript : MonoBehaviour
{
    [SerializeField]
    GameObject attackCollider;

    private const float maxAttackTime = 1.0f;
    private float attackTime = 0.0f;
    private bool attackChecker = false;

    private void Awake()
    {
        attackCollider = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        AttackTimer();
    }

    public void OnAttack(InputValue input)
    {
        if (input.isPressed && !attackChecker)
        {
            Debug.Log("player attack");
            attackTime = 0.0f;
            attackChecker = true;
            attackCollider.SetActive(true);
        }
    }

    private void CheckAttackDirection()
    {

    }
    /*
     *     public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }
     */

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
