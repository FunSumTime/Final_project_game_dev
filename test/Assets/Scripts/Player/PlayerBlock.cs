using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    public PlayerStats playerStats;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (playerStats == null)
        {
            playerStats = GetComponent<PlayerStats>();
        }
    }

    void Update()
    {
        // Right mouse button held = blocking
        bool isBlocking = Input.GetMouseButton(1);

        if (anim != null)
        {
            anim.SetBool("IsBlocking", isBlocking);
        }

        if (playerStats != null)
        {
            playerStats.isBlocking = isBlocking;
        }
    }
}
