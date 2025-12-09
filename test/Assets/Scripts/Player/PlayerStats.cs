using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float baseDamage = 10f;
    public float armor = 0f;
    public UIManager uiManager;
    public bool isBlocking = false;
    public float blockReduction = 0.7f;

    Animator anim;
    bool isDead = false;
    public bool IsDead => isDead;
    [Header("Ult")]
    public float ultMax = 100f;
    public float ultCurrent = 0f;
    public float ultGainPerKill = 25f;

    public void AddUltCharge(float amount)
    {
        ultCurrent = Mathf.Clamp(ultCurrent + amount, 0f, ultMax);
        uiManager?.SetUlt(ultCurrent, ultMax);
    }


    void Start()
    {
        currentHealth = maxHealth;
        uiManager?.SetPlayerHealth(currentHealth, maxHealth);

        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        float finalDamage = amount;

        // armor
        finalDamage = Mathf.Max(0f, finalDamage - armor);

        // block reduction
        if (isBlocking)
        {
            finalDamage *= (1f - blockReduction);  // e.g. 30% of original
        }

        currentHealth -= finalDamage;
        currentHealth = Mathf.Max(0f, currentHealth);

        uiManager?.SetPlayerHealth(currentHealth, maxHealth);

        if (currentHealth <= 0f)
        {
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

        // disable control stuff (like we had)
        var cc = GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        var move = GetComponent<PlayerControl>();
        if (move != null) move.enabled = false;

        var attack = GetComponent<PlayerAttack>();
        if (attack != null) attack.enabled = false;

        var look = GetComponent<PlayerLook>();
        if (look != null) look.enabled = false;

        // snap down to the ground once
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 5f))
        {
            Vector3 p = transform.position;
            p.y = hit.point.y;
            transform.position = p;
        }

        Debug.Log("Player died");
    }



}
