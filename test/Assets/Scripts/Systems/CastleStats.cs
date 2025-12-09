using UnityEngine;

public class CastleStats : MonoBehaviour
{
    public float maxHealth = 500f;
    public float currentHealth;
    public UIManager uiManager;

    AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();

        Debug.Log("CastleStats Start on: " + gameObject.name);

        if (uiManager != null)
        {
            uiManager.SetCastleHealth(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(0f, currentHealth);

        Debug.Log($"Castle hit! Took {amount}, HP: {currentHealth}/{maxHealth}");

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }

        if (uiManager != null)
        {
            uiManager.SetCastleHealth(currentHealth, maxHealth);
        }

        if (currentHealth <= 0f)
        {
            OnCastleDestroyed();
        }
    }

    void OnCastleDestroyed()
    {
        Debug.Log("Castle destroyed! TODO: show Game Over UI");
    }
}
