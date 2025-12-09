using UnityEngine;
using System.Collections.Generic;

public class SwordHitbox : MonoBehaviour
{
    public float damage = 20f;
    public bool isActive = false;

    public AudioSource hitAudio;   // drag AudioSource here in Inspector

    HashSet<EnemyStats> hitThisSwing = new HashSet<EnemyStats>();

    public void BeginSwing()
    {
        isActive = true;
        hitThisSwing.Clear();
        Debug.Log("Sword swing START");
    }

    public void EndSwing()
    {
        isActive = false;
        Debug.Log("Sword swing END");
    }

    void TryHit(Collider other)
    {
        if (!isActive) return;

        EnemyStats enemy = other.GetComponent<EnemyStats>();
        if (enemy == null)
        {
            enemy = other.GetComponentInParent<EnemyStats>();
        }

        if (enemy != null && !hitThisSwing.Contains(enemy))
        {
            enemy.TakeDamage(damage);
            hitThisSwing.Add(enemy);

            Debug.Log("Sword hit enemy: " + enemy.gameObject.name);

            // play hit sound
            if (hitAudio != null && hitAudio.clip != null)
            {
                hitAudio.PlayOneShot(hitAudio.clip);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        TryHit(other);
    }

    void OnTriggerStay(Collider other)
    {
        TryHit(other);
    }
}
