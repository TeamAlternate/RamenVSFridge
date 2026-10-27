using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpecialAttackCooldownUI : MonoBehaviour
{
    private enum CharacterType
    {
        Ramen,
        Fridge
    }

    [SerializeField] private CharacterType character;
    [SerializeField] private Image cooldownImage;
    [SerializeField] private UnityEvent chargedSound;
    [SerializeField] private UnityEvent specialSound;

    private Image backgroundImage;
    private RamenScript ramen;
    private FridgeScript fridge;
    private bool playedRamenChargedSound = true;
    private bool playedFridgeChargedSound = true;
    private bool playedRamenSpecialSound = true;
    private bool playedFridgeSpecialSound = true;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        backgroundImage.enabled = false;
        cooldownImage.type = Image.Type.Filled;
        cooldownImage.fillMethod = Image.FillMethod.Radial360;
    }

    private void Update()
    {
        if (character == CharacterType.Ramen)
        {
            if (ramen == null)
            {
                ramen = FindFirstObjectByType<RamenScript>();
            }

            cooldownImage.fillAmount = ramen != null ? ramen.SpecialCooldownFill : 1f;

            if (ramen != null)
            {
                if( ramen.SpecialCooldownFill < 1.0f )
                {
                    playedRamenChargedSound = false;
                }

                if( ramen.SpecialCooldownFill >= 1.0f )
                {
                    playedRamenSpecialSound = false;
                }

                if( ramen.SpecialCooldownFill < 1.0f && !playedRamenSpecialSound )
                {
                    specialSound.Invoke();
                    playedRamenSpecialSound = true;
                }

                if (ramen.SpecialCooldownFill >= 1.0f && !playedRamenChargedSound)
                {
                    chargedSound.Invoke();
                    playedRamenChargedSound = true;
                }
            }
        }
        else
        {
            if (fridge == null)
            {
                fridge = FindFirstObjectByType<FridgeScript>();
            }

            cooldownImage.fillAmount = fridge != null ? fridge.SpecialCooldownFill : 1f;

            if (fridge != null)
            {
                if( fridge.SpecialCooldownFill < 1.0f )
                {
                    playedFridgeChargedSound = false;
                }

                if (fridge.SpecialCooldownFill >= 1.0f)
                {
                    playedFridgeSpecialSound = false;
                }

                if (fridge.SpecialCooldownFill < 1.0f && !playedFridgeSpecialSound)
                {
                    specialSound.Invoke();
                    playedFridgeSpecialSound = true;
                }

                if (fridge.SpecialCooldownFill >= 1.0f && !playedFridgeChargedSound)
                {
                    chargedSound.Invoke();
                    playedFridgeChargedSound = true;
                }
            }
        }

        backgroundImage.enabled = cooldownImage.fillAmount < 1f;
    }
}
