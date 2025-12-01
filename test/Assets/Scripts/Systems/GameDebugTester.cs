using UnityEngine;

public class GameDebugTester : MonoBehaviour
{
    public UIManager uiManager;

    int currentWave = 1;
    int enemiesRemaining = 10;
    int gold = 0;

    void Start()
    {
        if (uiManager != null)
        {
            uiManager.SetWave(currentWave);
            uiManager.SetEnemiesRemaining(enemiesRemaining);
            uiManager.SetGold(gold);
        }
    }

    void Update()
    {
        if (uiManager == null) return;

        // G = add gold
        if (Input.GetKeyDown(KeyCode.G))
        {
            gold += 10;
            uiManager.SetGold(gold);
        }

        // N = next wave
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentWave++;
            uiManager.SetWave(currentWave);
        }

        // E = enemy died
        if (Input.GetKeyDown(KeyCode.E))
        {
            enemiesRemaining = Mathf.Max(0, enemiesRemaining - 1);
            uiManager.SetEnemiesRemaining(enemiesRemaining);
        }
    }
}
