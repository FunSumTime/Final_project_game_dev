using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("References")]
    public WaveManager waveManager;
    public PlayerStats playerStats;
    public GameObject upgradePanel;

    [Header("Buttons")]
    public Button btnDamage;
    public Button btnHealth;
    public Button btnArmor;

    [Header("Costs")]
    public int damageCost = 20;
    public int healthCost = 25;
    public int armorCost = 30;

    bool isOpen = false;

    void Start()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }

        if (btnDamage != null) btnDamage.onClick.AddListener(UpgradeDamage);
        if (btnHealth != null) btnHealth.onClick.AddListener(UpgradeHealth);
        if (btnArmor != null) btnArmor.onClick.AddListener(UpgradeArmor);
    }

    void Update()
    {
        // Toggle upgrade menu with U key
        if (Input.GetKeyDown(KeyCode.U))
        {
            TogglePanel();
        }
    }

    void TogglePanel()
    {
        if (upgradePanel == null) return;

        isOpen = !isOpen;
        upgradePanel.SetActive(isOpen);

        // Optional: pause game when menu open
        Time.timeScale = isOpen ? 0f : 1f;
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void UpgradeDamage()
    {
        if (waveManager != null && playerStats != null)
        {
            if (waveManager.TrySpendGold(damageCost))
            {
                playerStats.baseDamage += 5f;
                Debug.Log("Damage upgraded! New baseDamage = " + playerStats.baseDamage);
            }
            else
            {
                Debug.Log("Not enough gold for damage upgrade");
            }
        }
    }

    void UpgradeHealth()
    {
        if (waveManager != null && playerStats != null)
        {
            if (waveManager.TrySpendGold(healthCost))
            {
                playerStats.maxHealth += 20f;
                playerStats.currentHealth = playerStats.maxHealth;
                playerStats.uiManager?.SetPlayerHealth(playerStats.currentHealth, playerStats.maxHealth);
                Debug.Log("Health upgraded! New maxHealth = " + playerStats.maxHealth);
            }
            else
            {
                Debug.Log("Not enough gold for health upgrade");
            }
        }
    }

    void UpgradeArmor()
    {
        if (waveManager != null && playerStats != null)
        {
            if (waveManager.TrySpendGold(armorCost))
            {
                playerStats.armor += 1f;
                Debug.Log("Armor upgraded! New armor = " + playerStats.armor);
            }
            else
            {
                Debug.Log("Not enough gold for armor upgrade");
            }
        }
    }
}
