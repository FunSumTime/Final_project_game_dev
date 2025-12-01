using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Tooltip("The Image component used as the fill (the red part).")]
    public Image fillImage;   // drag Fill here in the Inspector

    void Awake()
    {
        if (fillImage == null)
        {
            Debug.LogError("HealthBar on " + gameObject.name + " has no fillImage assigned!");
        }
        else if (fillImage.type != Image.Type.Filled)
        {
            Debug.LogWarning("HealthBar: " + gameObject.name + " fillImage is not set to Filled. Fixing it.");
            fillImage.type = Image.Type.Filled;
            fillImage.fillMethod = Image.FillMethod.Horizontal;
            fillImage.fillOrigin = (int)Image.OriginHorizontal.Left;
        }
    }

    public void SetHealth(float current, float max)
    {
        if (fillImage == null || max <= 0f) return;

        float pct = Mathf.Clamp01(current / max);

        fillImage.fillAmount = pct;

        // Debug so we can see it's being called
        // (you can delete later)
        Debug.Log($"{gameObject.name} health bar updated: {current}/{max} -> {pct}");
    }
}
