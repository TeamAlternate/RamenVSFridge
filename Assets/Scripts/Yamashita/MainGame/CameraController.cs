using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController instance;

    [SerializeField] private Camera cam;
    [SerializeField] private Transform focusPivot;

    [SerializeField] private float chaseSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [Range(40.0f, 180.0f)]
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
        Vector3 offset = cam.transform.rotation * Vector3.back * currentDistance;
        Vector3 nextPosition = Vector3.Lerp(focusCenter, Vector3.ProjectOnPlane(focusPivot.transform.position, Vector3.up), Mathf.Exp(-chaseSpeed * Time.deltaTime));
        focusPivot.transform.position = nextPosition;
        cam.transform.localPosition = offset;

        float targetDistance = minDistance;
        foreach(GameObject target in targets)
        {
            Vector3 targetToFocus = focusCenter - target.transform.position;
            targetDistance = Mathf.Max(Mathf.Tan((90.0f - baseFOV * 0.5f) * Mathf.Deg2Rad) * targetToFocus.magnitude, targetDistance);
        }
        targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
        currentDistance = Mathf.Lerp(targetDistance, currentDistance, Mathf.Exp(-zoomSpeed*  Time.deltaTime));
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
