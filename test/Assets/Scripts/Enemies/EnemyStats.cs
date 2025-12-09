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
    public bool IsDead => isDead;

    Animator anim;
    EnemyAI ai;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        ai = GetComponent<EnemyAI>();

        // Hook the hitbox back to this enemy
        EnemyHitbox hb = GetComponentInChildren<EnemyHitbox>();
        if (hb != null)
        {
            hb.owner = this;
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log($"Enemy {gameObject.name} took {amount} damage. HP now: {currentHealth}/{maxHealth}");

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

        if (ai != null)
        {
            ai.enabled = false;
        }

        if (waveManager != null)
        {
            waveManager.OnEnemyKilled(goldReward);
        }
        if (waveManager != null && waveManager.playerStats != null)
        {
            waveManager.playerStats.AddUltCharge(waveManager.playerStats.ultGainPerKill);
        }

        Destroy(gameObject, 3f);
    }
}
