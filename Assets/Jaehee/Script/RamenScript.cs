using UnityEngine;
using UnityEngine.InputSystem;

public class RamenScript : MonoBehaviour
{
    [SerializeField]
    GameObject attackCollider;

    [SerializeField]
    GameObject ramenAttackEffect;

    private const float maxAttackTime = 2.0f;
    private float attackTime = 0.0f;
    private bool attackChecker = false;

    //private const float reheatInterval = 2f;
    //private float reheatTimer = 0f;

    private void Awake()
    {
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
            Debug.Log("Ramen attack");
            attackTime = 0.0f;
            attackChecker = true;
            attackCollider.SetActive(true);
            ramenAttackEffect.GetComponent<ParticleSystem>().Play();
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
