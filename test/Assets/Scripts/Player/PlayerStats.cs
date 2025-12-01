using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float baseDamage = 10f;
    public float armor = 0f;
    public UIManager uiManager;

    Animator anim;
    bool isDead = false;
    public bool IsDead => isDead;

    void Start()
    {
        currentHealth = maxHealth;
        uiManager?.SetPlayerHealth(currentHealth, maxHealth);

        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        float finalDamage = Mathf.Max(0f, amount - armor);
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
