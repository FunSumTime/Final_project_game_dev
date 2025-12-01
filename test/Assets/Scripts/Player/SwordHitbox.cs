using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public float damage = 20f;
    public bool isActive = false;

    void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        EnemyStats enemy = other.GetComponent<EnemyStats>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
