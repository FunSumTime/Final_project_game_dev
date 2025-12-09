using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public SwordHitbox swordHitbox;
    public float attackCooldown = 0.6f;
    public float hitboxStart = 0.1f;   // when in the anim the blade is "in front"
    public float hitboxDuration = 0.35f;

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

        anim?.SetTrigger("Attack");

        // small delay before the hit starts, so it lines up with the swing
        yield return new WaitForSeconds(hitboxStart);

        if (swordHitbox != null) swordHitbox.BeginSwing();

        yield return new WaitForSeconds(hitboxDuration);

        if (swordHitbox != null) swordHitbox.EndSwing();

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
