using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public SwordHitbox swordHitbox;
    public float attackCooldown = 0.6f;
    public float hitboxDuration = 0.25f;

    Animator anim;
    bool canAttack = true;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(DoAttack());
        }
    }

    IEnumerator DoAttack()
    {
        canAttack = false;

        if (anim != null)
        {
            anim.SetTrigger("Attack");   // uses the Trigger we created
        }

        if (swordHitbox != null)
        {
            swordHitbox.isActive = true;
        }

        yield return new WaitForSeconds(hitboxDuration);

        if (swordHitbox != null)
        {
            swordHitbox.isActive = false;
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
