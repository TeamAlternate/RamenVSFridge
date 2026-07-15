using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController instance;

    private Camera cam;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float baseFOV;
    private List<GameObject> targets = new List<GameObject>();

    private float currentDistance;

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
        currentDistance = minDistance;
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
        focusCenter = Vector3.ProjectOnPlane(focusCenter, Vector3.up);
        Vector3 offset = this.transform.rotation * Vector3.back * currentDistance;
        Vector3 nextPosition = Vector3.Lerp(focusCenter, Vector3.ProjectOnPlane(this.transform.position - offset, Vector3.up), Mathf.Exp(-chaseSpeed * Time.deltaTime));
        this.transform.position = nextPosition + offset;

        float targetDistance = minDistance;
        foreach(GameObject target in targets)
        {
            Vector3 targetToFocus = nextPosition - target.transform.position;
            targetDistance = Mathf.Max(Mathf.Tan((90.0f - baseFOV * 0.5f) * Mathf.Deg2Rad) * targetToFocus.magnitude, targetDistance);
        }
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
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
