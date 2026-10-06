using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] private float jumpPower = 5f;

    [Header("Stun Visual")]
    [SerializeField] private float blinkInterval = 0.25f;
    [Range(0f, 1f)] [SerializeField] private float blinkMinOpacity = 0.1f;

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
    private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");
    private static readonly int ColorId = Shader.PropertyToID("_Color");
    private static readonly int SurfaceId = Shader.PropertyToID("_Surface");
    private static readonly int ModeId = Shader.PropertyToID("_Mode");
    private static readonly int BlendId = Shader.PropertyToID("_Blend");
    private static readonly int SrcBlendId = Shader.PropertyToID("_SrcBlend");
    private static readonly int DstBlendId = Shader.PropertyToID("_DstBlend");
    private static readonly int ZWriteId = Shader.PropertyToID("_ZWrite");
    private readonly List<BlinkMaterial> blinkMaterials = new List<BlinkMaterial>();
    private bool blinkActive;
    private float blinkStartTime;

    private sealed class BlinkMaterial
    {
        public Renderer Renderer;
        public int MaterialIndex;
        public int ColorProperty;
        public Color OriginalColor;
        public Material OriginalMaterial;
        public Material FadeMaterial;
        public MaterialPropertyBlock OriginalProperties;
        public MaterialPropertyBlock BlinkProperties;
    }

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
            RestoreCharacterVisuals();
            Destroy(currentCharacter);
        }

        currentCharacter = Instantiate(characterPrefab, transform);
        currentCharacter.transform.localPosition = Vector3.zero;
        currentCharacter.transform.localRotation = Quaternion.identity;
        CacheCharacterMaterials();
        if (blinkActive)
        {
            BeginTransparentBlink();
            UpdateBlink();
        }
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

        if (!IsStunned)
        {
            blinkStartTime = Time.time;
            blinkActive = true;
            BeginTransparentBlink();
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

    private void Update()
    {
        if (blinkActive)
        {
            UpdateBlink();
        }
    }

    private void OnEnable()
    {
        if (IsStunned)
        {
            blinkActive = true;
            BeginTransparentBlink();
            UpdateBlink();
        }
    }

    private void OnDisable()
    {
        RestoreCharacterVisuals();
        blinkActive = false;
    }

    private void CacheCharacterMaterials()
    {
        blinkMaterials.Clear();
        Renderer[] allRenderers = currentCharacter.GetComponentsInChildren<Renderer>(true);

        foreach (Renderer visual in allRenderers)
        {
            if (!(visual is MeshRenderer || visual is SkinnedMeshRenderer) || !visual.enabled)
            {
                continue;
            }

            Material[] materials = visual.sharedMaterials;
            for (int index = 0; index < materials.Length; index++)
            {
                Material material = materials[index];
                if (material == null)
                {
                    continue;
                }

                int colorProperty = material.HasProperty(BaseColorId) ? BaseColorId : ColorId;
                if (!material.HasProperty(colorProperty))
                {
                    continue;
                }

                BlinkMaterial entry = new BlinkMaterial
                {
                    Renderer = visual,
                    MaterialIndex = index,
                    ColorProperty = colorProperty,
                    OriginalColor = material.GetColor(colorProperty),
                    OriginalMaterial = material,
                    OriginalProperties = new MaterialPropertyBlock(),
                    BlinkProperties = new MaterialPropertyBlock()
                };
                visual.GetPropertyBlock(entry.OriginalProperties, index);
                visual.GetPropertyBlock(entry.BlinkProperties, index);
                blinkMaterials.Add(entry);
            }
        }
    }

    private void BeginTransparentBlink()
    {
        foreach (BlinkMaterial entry in blinkMaterials)
        {
            if (entry.Renderer == null || entry.FadeMaterial != null)
            {
                continue;
            }

            Material fade = new Material(entry.OriginalMaterial);
            ConfigureTransparent(fade);
            entry.FadeMaterial = fade;

            Material[] materials = entry.Renderer.sharedMaterials;
            materials[entry.MaterialIndex] = fade;
            entry.Renderer.sharedMaterials = materials;
        }
    }

    private static void ConfigureTransparent(Material material)
    {
        if (material.HasProperty(SurfaceId))
        {
            material.SetFloat(SurfaceId, 1f);
            if (material.HasProperty(BlendId)) material.SetFloat(BlendId, 0f);
            material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        }
        else if (material.HasProperty(ModeId))
        {
            material.SetFloat(ModeId, 2f);
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        }

        if (material.HasProperty(SrcBlendId)) material.SetInt(SrcBlendId, (int)BlendMode.SrcAlpha);
        if (material.HasProperty(DstBlendId)) material.SetInt(DstBlendId, (int)BlendMode.OneMinusSrcAlpha);
        if (material.HasProperty(ZWriteId)) material.SetInt(ZWriteId, 0);
        material.SetOverrideTag("RenderType", "Transparent");
        material.renderQueue = (int)RenderQueue.Transparent;
    }

    private void UpdateBlink()
    {
        if (!IsStunned)
        {
            RestoreCharacterVisuals();
            blinkActive = false;
            return;
        }

        float interval = Mathf.Max(0.02f, blinkInterval);
        float phase = (Time.time - blinkStartTime) * Mathf.PI / interval;
        float opacity = Mathf.Lerp(blinkMinOpacity, 1f, (Mathf.Cos(phase) + 1f) * 0.5f);

        foreach (BlinkMaterial entry in blinkMaterials)
        {
            if (entry.Renderer == null)
            {
                continue;
            }

            Color color = entry.OriginalColor;
            color.a *= opacity;
            entry.BlinkProperties.SetColor(entry.ColorProperty, color);
            entry.Renderer.SetPropertyBlock(entry.BlinkProperties, entry.MaterialIndex);
        }
    }

    private void RestoreCharacterVisuals()
    {
        foreach (BlinkMaterial entry in blinkMaterials)
        {
            if (entry.FadeMaterial == null)
            {
                continue;
            }

            if (entry.Renderer != null)
            {
                Material[] materials = entry.Renderer.sharedMaterials;
                materials[entry.MaterialIndex] = entry.OriginalMaterial;
                entry.Renderer.sharedMaterials = materials;
                entry.Renderer.SetPropertyBlock(entry.OriginalProperties, entry.MaterialIndex);
            }

            Destroy(entry.FadeMaterial);
            entry.FadeMaterial = null;
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
