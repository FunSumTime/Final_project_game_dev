using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 30f;
    public int goldReward = 5;

    [Header("References")]
    public WaveManager waveManager;

    float currentHealth;
    bool isDead = false;
    Animator anim;
    EnemyAI ai;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        ai = GetComponent<EnemyAI>();
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        if (anim != null)
        {
            anim.SetBool("IsDead", true);
            anim.SetFloat("Speed", 0f);
        }

        // stop AI movement/attacks
        if (ai != null)
        {
            ai.enabled = false;
        }

        // tell WaveManager
        if (waveManager != null)
        {
            waveManager.OnEnemyKilled(goldReward);
        }

        // destroy after animation finishes
        Destroy(gameObject, 3f);
    }
}
