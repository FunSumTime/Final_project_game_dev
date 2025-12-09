using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public float damageToPlayer = 5f;
    public float damageToCastle = 1f;

    [HideInInspector] public bool isActive = false;
    [HideInInspector] public EnemyStats owner;

    void OnTriggerEnter(Collider other)
    {
        if (!isActive || owner == null || owner.IsDead) return;

        Debug.Log("EnemyHitbox trigger with: " + other.name);

        // Player
        PlayerStats player =
            other.GetComponent<PlayerStats>() ??
            other.GetComponentInParent<PlayerStats>();

        if (player != null)
        {
            player.TakeDamage(damageToPlayer);
            Debug.Log("Enemy punch hit PLAYER");
            return;
        }

        // Castle
        CastleStats castle =
            other.GetComponent<CastleStats>() ??
            other.GetComponentInParent<CastleStats>();

        if (castle != null)
        {
            castle.TakeDamage(damageToCastle);
            Debug.Log("Enemy punch hit CASTLE");
            return;
        }
    }
}
