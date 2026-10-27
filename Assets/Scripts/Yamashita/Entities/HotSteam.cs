using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class HotSteam : MonoBehaviour
{
    [SerializeField] private float trailLifetimeOnFullHeat;
    [SerializeField] private float steamUpwardingSpeed;

    private Transform sourcePosition;
    private TimeManager timeManager;
    private TrailRenderer trailRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeManager = TimeManager.instance;
        trailRenderer = GetComponent<TrailRenderer>();
        sourcePosition = this.transform.parent;
        this.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = sourcePosition.position;
        for(int i = 0; i < trailRenderer.positionCount; i++)
        {
            trailRenderer.SetPosition(i, trailRenderer.GetPosition(i) + steamUpwardingSpeed * Time.deltaTime * Vector3.up);
        }

        float time = 180.0f;
        if (timeManager != null)
        {
            time = timeManager.time;
        }

        trailRenderer.time = Mathf.Lerp(0.0f, trailLifetimeOnFullHeat, time / 180.0f);
    }
}
