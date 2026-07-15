using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    private static CameraControler instance;

    private Camera cam;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float distance;
    [SerializeField] private float focusMergin;
    private List<GameObject> targets;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targets.Count == 0)
        {
            return;
        }
        Vector3 focusCenter = Vector3.zero;
        foreach (GameObject target in targets)
        {
            focusCenter += target.transform.position;
        }
        focusCenter /= targets.Count;
        Vector3 nextPosition = Vector3.Lerp(this.transform.position, focusCenter, Mathf.Exp(-chaseSpeed * Time.deltaTime));
        this.transform.position = nextPosition + this.transform.rotation * Vector3.forward * distance;


        //cam.fieldOfView = ;
    }

    public static void AddTarget(GameObject newTarget)
    {
        instance.targets.Add(newTarget);
    }

    public static void RemoveTarget(GameObject removeTarget)
    {
        instance.targets.Remove(removeTarget);
    }
}
