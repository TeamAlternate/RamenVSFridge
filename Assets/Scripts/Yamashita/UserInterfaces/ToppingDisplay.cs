using UnityEngine;

public class ToppingDisplay : MonoBehaviour
{
    [SerializeField] private ToppingDisplayElement elementPrefab;
    [SerializeField] private int maxInstanceCount;

    [SerializeField] private Transform alignmentPivot;
    [SerializeField] private Vector3 offsetPerTopping;
    private ToppingDisplayElement[] elements;



    public void AddTopping()
    {

    }
}
