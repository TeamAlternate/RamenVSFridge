using UnityEngine;
using UnityEngine.InputSystem;

public class FridgeScript : MonoBehaviour
{
    CapsuleCollider boxCollider;

    [SerializeField]
    GameObject attackCollider;
    [SerializeField]
    GameObject specialAttackCollider;

    [SerializeField]
    GameObject fridgeAttackEffect;
    [SerializeField]
    GameObject fridgeSpecialAttackEffect;

    // UŒ‚ŠÖ˜A
    private const float maxAttackTime = 2.0f;
    private float attackTime = 0.0f;
    private bool attackChecker = false;

    // •KŽE‹ZŠÖ˜A
    private const float maxSpecialTime = 30.0f;
    private const float specialAttackDuration = 2.0f;
    private float specialAttackTime = 0.0f;
    private bool specialAttackChecker = false;

    public float SpecialCooldownFill => specialAttackChecker
        ? Mathf.Clamp01(specialAttackTime / maxSpecialTime)
        : 1f;

    private void Awake()
    {
        // ground collider pivot
        boxCollider = transform.parent.GetComponent<CapsuleCollider>();
        boxCollider.center = new Vector3(0.0f, -0.5f, 0.0f);

        if (attackCollider)
        {
            attackCollider.SetActive(false);
        }

        if (specialAttackCollider)
        {
            specialAttackCollider.SetActive(false);
        }
    }

    private void Update()
    {
        AttackTimer();
        SpecialAttackTimer();
    }

    public void OnAttack(InputValue input)
    {
        if (input.isPressed && !attackChecker && GetComponentInParent<PlayerMovement>()?.IsStunned != true)
        {
            Debug.Log("Fridge attack");
            attackTime = 0.0f;
            attackChecker = true;
            attackCollider.SetActive(true);
            fridgeAttackEffect.GetComponent<ParticleSystem>().Play();
        }
    }

    public void OnSpecialAttack(InputValue input)
    {
        if (input.isPressed && !specialAttackChecker && GetComponentInParent<PlayerMovement>()?.IsStunned != true)
        {
            Debug.Log("Fridge Special Attack");
            specialAttackTime = 0.0f;
            specialAttackChecker = true;
            specialAttackCollider.SetActive(true);
            fridgeSpecialAttackEffect.GetComponent<ParticleSystem>().Play(true);
        }
    }

    public void CancelAttacks()
    {
        attackCollider.SetActive(false);
        specialAttackCollider.SetActive(false);
        fridgeAttackEffect.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        fridgeSpecialAttackEffect.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
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

    private void SpecialAttackTimer()
    {
        if (!specialAttackChecker)
        {
            return;
        }

        specialAttackTime += Time.deltaTime;

        if (specialAttackTime >= specialAttackDuration && specialAttackCollider.activeSelf)
        {
            specialAttackCollider.SetActive(false);
        }

        if (specialAttackTime >= maxSpecialTime)
        {
            specialAttackChecker = false;
        }
    }
}
