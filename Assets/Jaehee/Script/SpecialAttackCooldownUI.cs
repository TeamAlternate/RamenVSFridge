using UnityEngine;
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

    private Image backgroundImage;
    private RamenScript ramen;
    private FridgeScript fridge;

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
        }
        else
        {
            if (fridge == null)
            {
                fridge = FindFirstObjectByType<FridgeScript>();
            }

            cooldownImage.fillAmount = fridge != null ? fridge.SpecialCooldownFill : 1f;
        }

        backgroundImage.enabled = cooldownImage.fillAmount < 1f;
    }
}
