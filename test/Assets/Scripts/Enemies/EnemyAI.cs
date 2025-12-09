using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyAI : MonoBehaviour
{
    [Header("Targets")]
    public Transform playerTarget;
    public Transform castleTarget;
    public float playerChaseRadius = 8f;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float attackDistance = 3f;

    [Header("Attack timing")]
    public float attackWindup = 0.2f;
    public float attackActiveTime = 0.3f;
    public float attackCooldown = 0.8f;

    [Header("Hitbox")]
    public EnemyHitbox punchHitbox;

    CharacterController controller;
    Animator anim;

    float gravity = -9.81f;
    float verticalVelocity = 0f;
    bool isAttacking = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // gravity
        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        if (isAttacking)
        {
            Vector3 fallOnly = new Vector3(0f, verticalVelocity, 0f);
            controller.Move(fallOnly * Time.deltaTime);
            return;
        }

        Transform currentTarget = GetCurrentTarget();
        if (currentTarget == null) return;

        Vector3 toTarget = currentTarget.position - transform.position;
        toTarget.y = 0f;
        float dist = toTarget.magnitude;

        Vector3 move = Vector3.zero;
        float speedParam = 0f;

        if (dist > attackDistance)
        {
            Vector3 dir = toTarget.normalized;
            move = dir * moveSpeed;
            speedParam = 1f;

            if (dir.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(dir);
            }
        }
        else
        {
            if (!isAttacking)
            {
                StartCoroutine(AttackRoutine());
            }
        }

        if (anim != null)
        {
            anim.SetFloat("Speed", speedParam);
        }

        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }

    Transform GetCurrentTarget()
    {
        // If player is alive and within radius, prefer him
        if (playerTarget != null)
        {
            float distToPlayer = Vector3.Distance(transform.position, playerTarget.position);
            PlayerStats ps = playerTarget.GetComponent<PlayerStats>();
            bool playerAlive = (ps == null || !ps.IsDead);

            if (playerAlive && distToPlayer <= playerChaseRadius)
            {
                return playerTarget;
            }
        }

        // Otherwise walk toward castle
        if (castleTarget != null)
        {
            return castleTarget;
        }

        return null;
    }


    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        if (anim != null)
        {
            anim.SetTrigger("Attack");
            Debug.Log("Enemy starting ATTACK routine");

        }

        // windup
        yield return new WaitForSeconds(attackWindup);

        // hitbox on
        if (punchHitbox != null) punchHitbox.isActive = true;

        yield return new WaitForSeconds(attackActiveTime);

        // hitbox off
        if (punchHitbox != null) punchHitbox.isActive = false;

        // cooldown
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }
}
