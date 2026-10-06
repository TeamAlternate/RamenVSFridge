using UnityEngine;
using UnityEngine.InputSystem;

public class RamenScript : MonoBehaviour
{
    [SerializeField]
    GameObject attackCollider;
    [SerializeField]
    GameObject specialAttackCollider;

    [SerializeField]
    GameObject ramenAttackEffect;
    [SerializeField]
    GameObject ramenSpecialAttackEffect;

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
            Debug.Log("Ramen attack");
            attackTime = 0.0f;
            attackChecker = true;
            attackCollider.SetActive(true);
            ramenAttackEffect.GetComponent<ParticleSystem>().Play();
        }
    }

    public void OnSpecialAttack(InputValue input)
    {
        if (input.isPressed && !specialAttackChecker && GetComponentInParent<PlayerMovement>()?.IsStunned != true)
        {
            Debug.Log("Ramen Special Attack");
            specialAttackTime = 0.0f;
            specialAttackChecker = true;
            specialAttackCollider.SetActive(true);
            ramenSpecialAttackEffect.GetComponent<ParticleSystem>().Play(true);
        }
    }

    public void CancelAttacks()
    {
        attackCollider.SetActive(false);
        specialAttackCollider.SetActive(false);
        ramenAttackEffect.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        ramenSpecialAttackEffect.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            TimeManager.instance.ChangeTimeState(TimeState.Decele);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            TimeManager.instance.ChangeTimeState(TimeState.Normal);
        }
    }

}
