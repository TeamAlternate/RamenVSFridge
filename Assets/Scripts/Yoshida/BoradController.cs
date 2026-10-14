using UnityEngine;

public class BoradController : MonoBehaviour
{
    [SerializeField] private MeshRenderer spriteRenderer;
    [SerializeField] private float flashInterval;
    [SerializeField] private Material beforeMaterial;
    [SerializeField] private Material changeMaterial;

    bool isLight = true;
    float time = 0.0f;
    Material material;

    private void Awake()
    {
        material = beforeMaterial;
        spriteRenderer.material = material;
    }

    // Update is called once per frame
    void Update()
    {
        if( time >= flashInterval )
        {
            if( isLight )
            {
                isLight = false;
            }
            else
            {
                isLight = true;
            }
            time = 0.0f;
        }

        UpdateMaterial(time / flashInterval);
        time += Time.deltaTime;
    }

    private void UpdateMaterial(float percent)
    {
        Color color = spriteRenderer.material.color;

        if ( isLight ) 
        { 
            color.r = percent;
            color.g = percent;
            color.b = percent;
            color.a = percent;
        }
        else
        {
            color.r = 1.0f - percent;
            color.g = 1.0f - percent;
            color.b = 1.0f - percent;
            color.a = 1.0f - percent;
        }

        spriteRenderer.material.SetColor("_EmissionColor", color);
    }

    public void ChangeMaterial()
    {
        spriteRenderer.material = changeMaterial;
    }
}
