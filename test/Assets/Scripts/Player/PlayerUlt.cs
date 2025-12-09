using UnityEngine;
using System.Collections;

public class PlayerUlt : MonoBehaviour
{
    public PlayerStats playerStats;
    public float ultRadius = 6f;
    public LayerMask enemyLayer;
    public float castDelay = 0.4f;  // wait for animation windup
    public GameObject ultRingPrefab;  // assign your ring particle prefab here


    Animator anim;
    bool canUlt = true;

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
        if (Input.GetKeyDown(KeyCode.Q) && canUlt && playerStats != null)
        {
            if (playerStats.ultCurrent >= playerStats.ultMax)
            {
                StartCoroutine(DoUlt());
            }
        }
    }

    IEnumerator DoUlt()
    {
        canUlt = false;

        // reset charge & UI
        playerStats.ultCurrent = 0f;
        playerStats.uiManager?.SetUlt(0f, playerStats.ultMax);

        // play ult animation
        anim?.SetTrigger("Ult");

        // wait for the “impact” moment
        yield return new WaitForSeconds(castDelay);

        // spawn ring effect under player
        if (ultRingPrefab != null)
        {
            Instantiate(
                ultRingPrefab,
                new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z),
                Quaternion.Euler(90f, 0f, 0f)  // keep it flat
            );
        }


        // kill enemies around
        Collider[] hits = Physics.OverlapSphere(transform.position, ultRadius, enemyLayer);
        foreach (var col in hits)
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>() ?? col.GetComponentInParent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(99999f);
            }
        }

        yield return new WaitForSeconds(0.5f);
        canUlt = true;
    }

}
