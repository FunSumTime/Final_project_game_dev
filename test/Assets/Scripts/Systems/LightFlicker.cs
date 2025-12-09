using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float intensityBase = 2f;
    public float intensityRange = 0.5f;
    public float flickerSpeed = 10f;

    Light l;

    void Start()
    {
        l = GetComponent<Light>();
    }

    void Update()
    {
        if (l == null) return;
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0f);
        l.intensity = intensityBase + (noise - 0.5f) * intensityRange;
    }
}
