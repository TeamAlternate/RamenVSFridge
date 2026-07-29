using UnityEngine;
using UnityEngine.InputSystem;

public class RamenScript : MonoBehaviour
{
    [SerializeField]
    GameObject attackCollider;

    private const float maxAttackTime = 2.0f;
    private float attackTime = 0.0f;
    private bool attackChecker = false;

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
        // Ä‰Á”M
        if (other.gameObject.CompareTag("Fire"))
        {
            TimeManager.instance.ChangeTimeState(TimeState.Accele);
        }
    }


}
