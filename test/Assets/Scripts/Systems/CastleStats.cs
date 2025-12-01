using UnityEngine;

public class CastleStats : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 200f;
    public float currentHealth;

    [Header("References")]
    public UIManager uiManager;

    bool isDestroyed = false;

    void Start()
    {
        currentHealth = maxHealth;
        if (uiManager != null)
        {
            uiManager.SetCastleHealth(currentHealth, maxHealth);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(20f);
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDestroyed) return;

        currentHealth -= amount;
        if (currentHealth < 0f) currentHealth = 0f;

        if (uiManager != null)
        {
            uiManager.SetCastleHealth(currentHealth, maxHealth);
        }

        if (currentHealth <= 0f)
        {
            isDestroyed = true;
            Debug.Log("CASTLE DESTROYED!");
            // TODO: talk to GameManager / show Game Over later
        }
    }
}
