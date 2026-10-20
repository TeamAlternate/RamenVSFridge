using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class HotSteam : MonoBehaviour
{
    [SerializeField] private float trailLifetimeOnFullHeat;
    [SerializeField] private float steamUpwardingSpeed;

    private TimeManager timeManager;
    private TrailRenderer trailRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeManager = TimeManager.instance;
        trailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = trailRenderer.positionCount - 1; i >= 1; i--)
        {
            trailRenderer.SetPosition(i - 1, trailRenderer.GetPosition(i) + steamUpwardingSpeed * Time.deltaTime * Vector3.up);
        }
        trailRenderer.AddPosition(this.transform.position);
        trailRenderer.time = Mathf.Lerp(0.0f, trailLifetimeOnFullHeat, timeManager.time / 180.0f);
    }
}
