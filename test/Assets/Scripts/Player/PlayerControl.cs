using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float gravity = -9.81f;

    CharacterController controller;
    Animator anim;
    PlayerStats stats;

    float verticalVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();

        if (anim != null)
        {
            anim.applyRootMotion = false;
        }
    }

    void Update()
    {
        // Always apply gravity so the body stays on the ground
        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -1f;  // small downward force to keep grounded
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        // If dead: don't read input, don't move horizontally
        if (stats != null && stats.IsDead)
        {
            if (anim != null)
            {
                anim.SetFloat("Speed", 0f);
            }

            Vector3 fall = new Vector3(0f, verticalVelocity, 0f);
            controller.Move(fall * Time.deltaTime);
            return;
        }

        // ALIVE: normal movement
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = (transform.right * h + transform.forward * v);
        move = move.normalized;

        float currentSpeed = move.magnitude;
        if (anim != null)
        {
            anim.SetFloat("Speed", currentSpeed);
        }

        Vector3 velocity = move * moveSpeed;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }
}
