using UnityEngine;

public class FillCube : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material activeMat;
    [SerializeField] private Material inactiveMat;
    private float initScale = 8.0f;

    public void FillUpdate(float percent)
    {
        Vector3 newScale = this.transform.localScale;
        newScale.y = percent * initScale;
        this.transform.localScale = newScale;

        meshRenderer.material = activeMat;
    }

    public void FillReset()
    {
        Vector3 newScale = this.transform.localScale;
        newScale.y = initScale;
        this.transform.localScale = newScale;

        meshRenderer.material = inactiveMat;
    }
}
