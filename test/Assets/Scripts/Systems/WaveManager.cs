using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Spawning")]
    public EnemyStats enemyPrefab;
    public Transform[] spawnPoints;

    [Header("References")]
    public UIManager uiManager;
    public PlayerStats playerStats;
    public CastleStats castleStats;


    int currentWave = 1;
    int enemiesAlive = 0;
    int gold = 0;
    [Header("Difficulty")]
    public float enemyHealthPerWave = 1.2f;  // 20% more health per wave
    public float enemyDamagePerWave = 1.1f;  // 10% more damage per wave

    public int CurrentGold => gold;  
    void Start()
    {
        if (uiManager != null)
        {
            uiManager.SetWave(currentWave);
            uiManager.SetEnemiesRemaining(enemiesAlive);
            uiManager.SetGold(gold);
        }

        StartWave(currentWave);
    }

    void StartWave(int waveNumber)
    {
        enemiesAlive =  5 + (waveNumber - 1)* 2;
        if (uiManager != null)
        {
            uiManager.SetWave(currentWave);
        }


        for (int i = 0; i < enemiesAlive; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Vector3 spawnPos = GetFreeSpawnPosition(spawnPoint.position);

            EnemyStats enemy = Instantiate(enemyPrefab, spawnPos, spawnPoint.rotation);
            enemy.waveManager = this;


            // scale enemy stats with wave
            float healthMultiplier = Mathf.Pow(enemyHealthPerWave, waveNumber - 1);
            enemy.maxHealth *= healthMultiplier;


            EnemyHitbox hb = enemy.GetComponentInChildren<EnemyHitbox>();
            if (hb != null)
            {
                float dmgMult = Mathf.Pow(enemyDamagePerWave, waveNumber - 1);
                hb.damageToPlayer *= dmgMult;
            }

            EnemyAI ai = enemy.GetComponent<EnemyAI>();
            if (ai != null)
            {
                ai.playerTarget = playerStats.transform;
                ai.castleTarget = castleStats.transform;
            }
        }
    }

    Vector3 GetFreeSpawnPosition(Vector3 basePos)
    {
        const float radius = 1.5f;
        const int maxTries = 8;

        for (int i = 0; i < maxTries; i++)
        {
            Vector2 offset2D = Random.insideUnitCircle * radius;
            Vector3 pos = basePos + new Vector3(offset2D.x, 0f, offset2D.y);

            // check if another enemy is already here
            bool blocked = Physics.CheckSphere(pos, 0.8f, LayerMask.GetMask("enemy"));
            if (!blocked)
            {
                return pos;
            }
        }

        return basePos;
    }


    public void OnEnemyKilled(int goldReward)
    {
        enemiesAlive--;
        if (uiManager != null)
        {
            uiManager.SetEnemiesRemaining(enemiesAlive);
        }

        gold += goldReward;
        if (uiManager != null)
        {
            uiManager.SetGold(gold);
        }

        if (enemiesAlive <= 0)
        {
            currentWave++;
            StartWave(currentWave);
        }
    }
    public bool TrySpendGold(int amount)
    {
        if (gold < amount) return false;

        gold -= amount;
        if (uiManager != null)
        {
            uiManager.SetGold(gold);
        }
        return true;
    }

}
