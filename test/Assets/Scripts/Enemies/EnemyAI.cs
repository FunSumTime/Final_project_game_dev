using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyAI : MonoBehaviour
{
    public Transform target;          // player transform
    public float moveSpeed = 3f;
    public float attackDistance = 2f;
    public float timeBetweenAttacks = 1.0f;
    public float attackDamage = 5f;
    public PlayerStats playerStats;

    CharacterController controller;
    Animator anim;

    float gravity = -9.81f;
    float verticalVelocity = 0f;
    float attackTimer = 0f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (target == null) return;

        // basic gravity
        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 toTarget = target.position - transform.position;
        toTarget.y = 0f;
        float dist = toTarget.magnitude;

        Vector3 horizontalMove = Vector3.zero;
        float speedValue = 0f;

        if (dist > attackDistance)
        {
            // move toward player
            Vector3 dir = toTarget.normalized;
            horizontalMove = dir * moveSpeed;
            speedValue = 1f; // “moving”

            // rotate toward player
            if (dir.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(dir);
            }

            attackTimer = 0f; // reset
        }
        else
        {
            // in attack range
            horizontalMove = Vector3.zero;
            speedValue = 0f;

            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                DoAttack();
                attackTimer = timeBetweenAttacks;
            }
        }

        // apply animation Speed
        if (anim != null)
        {
            anim.SetFloat("Speed", speedValue);
        }

        Vector3 velocity = horizontalMove;
        velocity.y = verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }

    void DoAttack()
    {
        if (anim != null)
        {
            anim.SetTrigger("Attack");
        }

        if (playerStats != null)
        {
            // simple distance check to avoid weirdness
            float dist = Vector3.Distance(transform.position, playerStats.transform.position);
            if (dist <= attackDistance + 0.5f)
            {
                playerStats.TakeDamage(attackDamage);
            }
        }
    }
}
