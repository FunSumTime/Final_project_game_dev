using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Spawning")]
    public EnemyStats enemyPrefab;
    public Transform[] spawnPoints;

    [Header("References")]
    public UIManager uiManager;
    public PlayerStats playerStats;

    int currentWave = 1;
    int enemiesAlive = 0;
    int gold = 0;
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
        int enemiesToSpawn = 5 + (waveNumber - 1) * 2; // simple scaling

        enemiesAlive = enemiesToSpawn;
        uiManager.SetEnemiesRemaining(enemiesAlive);
        uiManager.SetWave(waveNumber);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

            EnemyStats enemy = Instantiate(enemyPrefab, spawn.position, spawn.rotation);

            // hook references
            enemy.waveManager = this;

            EnemyAI ai = enemy.GetComponent<EnemyAI>();
            if (ai != null)
            {
                ai.target = playerStats.transform;
                ai.playerStats = playerStats;
            }
        }
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
