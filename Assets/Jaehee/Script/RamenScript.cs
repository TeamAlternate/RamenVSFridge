using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField]
    GameObject freezedIceVisual;

    // UŒ‚ŠÖ˜A
    private const float maxAttackTime = 0.55f;
    private const float attackCoolTime = 1.75f;
    private float attackTime = 0.0f;
    private bool attackChecker = false;
    private float attackCool = 0.0f;

    // •KŽE‹ZŠÖ˜A
    private const float maxSpecialTime = 30.0f;
    private const float specialAttackDuration = 2.0f;
    private float specialAttackTime = 0.0f;
    private bool specialAttackChecker = false;

    public float SpecialCooldownFill => specialAttackChecker
        ? Mathf.Clamp01(specialAttackTime / maxSpecialTime)
        : 1f;

    private bool isFreezed;

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

        if(freezedIceVisual)
        {
            freezedIceVisual.SetActive(false);
        }
    }

    private void Update()
    {
        bool nowFreezed = false;
        if(TimeManager.instance != null)
        {
            nowFreezed = TimeManager.instance.time < 2.0f;
        }
        if (isFreezed != nowFreezed)
        {
            isFreezed = nowFreezed;
            freezedIceVisual.SetActive(isFreezed);
        }
        AttackTimer();
        SpecialAttackTimer();
    }

    public void OnAttack(InputValue input)
    {
        if(isFreezed)
        {
            return;
        }
        if (input.isPressed && !attackChecker && GetComponentInParent<PlayerMovement>()?.IsStunned != true)
        {
            Debug.Log("Ramen attack");
            attackTime = 0.0f;
            attackCool = attackCoolTime;
            attackChecker = true;
            attackCollider.SetActive(true);
            ramenAttackEffect.GetComponent<ParticleSystem>().Play();
        }
    }

    public void OnSpecialAttack(InputValue input)
    {
        if (isFreezed)
        {
            return;
        }
        if (input.isPressed && !specialAttackChecker && GetComponentInParent<PlayerMovement>()?.IsStunned != true)
        {
            Debug.Log("Ramen Special Attack");
            specialAttackTime = 0.0f;
            specialAttackChecker = true;
            specialAttackCollider.SetActive(true);
            ramenSpecialAttackEffect.GetComponent<ParticleSystem>().Play(true);
            TimeManager.instance.AddTime(0.15f);
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
        attackCool -= Time.deltaTime;

        if (attackTime >= maxAttackTime)
        {
            attackCollider.SetActive(false);
        }
        
        if( attackCool <= 0.0f)
        {
            attackChecker = false;
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
