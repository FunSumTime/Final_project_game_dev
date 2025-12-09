using UnityEngine;

public class UltRingFX : MonoBehaviour
{
    public float maxScale = 6f;      // how far out it grows
    public float duration = 0.5f;    // how long it lives

    float timer = 0f;
    Material mat;
    Color startColor;

    void Start()
    {
        // force a clean starting scale (tiny circle)
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            mat = rend.material;     // make unique instance
            startColor = mat.color;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);

        // uniform scale in X and Z so it stays a circle
        float s = Mathf.Lerp(0.1f, maxScale, t);
        transform.localScale = new Vector3(s, 0.1f, s);

        // fade alpha
        if (mat != null)
        {
            Color c = startColor;
            c.a = Mathf.Lerp(startColor.a, 0f, t);
            mat.color = c;
        }

        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}
