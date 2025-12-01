using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Player UI")]
    public HealthBar playerHealthBar;

    [Header("Castle UI")]
    public HealthBar castleHealthBar;

    [Header("Wave & Enemies")]
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI enemiesText;

    [Header("Gold")]
    public TextMeshProUGUI goldText;

    [Header("Weapon Info")]
    public TextMeshProUGUI weaponInfoText;


    public void SetPlayerHealth(float current, float max)
    {
        playerHealthBar.SetHealth(current, max);
    }

    public void SetCastleHealth(float current, float max)
    {
        castleHealthBar.SetHealth(current, max);
    }

    public void SetWave(int waveNumber)
    {
        waveText.text = $"Wave {waveNumber}";
    }

    public void SetEnemiesRemaining(int count)
    {
        enemiesText.text = $"Enemies: {count}";
    }

    public void SetGold(int amount)
    {
        goldText.text = amount.ToString();
    }

    public void SetWeaponInfo(string text)
    {
        weaponInfoText.text = text;
    }
}
