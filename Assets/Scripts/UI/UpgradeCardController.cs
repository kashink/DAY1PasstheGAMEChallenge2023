using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Upgrade
{
    ShieldQuant,
    ShieldSpeed,
    WeaponQuant,
    WeaponDamage,
    WeaponCadence
}

public class UpgradeCardController : MonoBehaviour
{
    [Space()]
    [Header("Config")]
    [SerializeField] Upgrade upgradeType;
    [SerializeField] TextMeshProUGUI upgradeCostText;
    [SerializeField] GameObject disabledObj;

    private void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        int upgradeCost = GetUpgradeCost();

        this.disabledObj.SetActive(PlayerStatsManager.Instance.totalCoin < upgradeCost || isMaxLeveled());
        this.upgradeCostText.text = $"{upgradeCost}";
    }

    int GetUpgradeCost()
    {
        switch (this.upgradeType)
        {
            case Upgrade.ShieldQuant:
                return PlayerStatsManager.Instance.shieldQuantLevel * 5;
            case Upgrade.ShieldSpeed:
                return PlayerStatsManager.Instance.shieldSpeedLevel * 5;
            case Upgrade.WeaponQuant:
                return PlayerStatsManager.Instance.weaponQuantLevel * 5;
            case Upgrade.WeaponDamage:
                return PlayerStatsManager.Instance.weaponDamageLevel * 5;
            case Upgrade.WeaponCadence:
                return PlayerStatsManager.Instance.weaponCadenceLevel * 5;
        }

        return 0;
    }

    bool isMaxLeveled()
    {
        switch (this.upgradeType)
        {
            case Upgrade.ShieldQuant:
                return PlayerStatsManager.Instance.shieldQuantLevel >= PlayerStatsManager.Instance.shieldQuantMaxLevel;
            case Upgrade.ShieldSpeed:
                return PlayerStatsManager.Instance.shieldSpeedLevel >= PlayerStatsManager.Instance.shieldSpeedMaxLevel;
            case Upgrade.WeaponQuant:
                return PlayerStatsManager.Instance.weaponQuantLevel >= PlayerStatsManager.Instance.weaponQuantMaxLevel;
            case Upgrade.WeaponDamage:
                return PlayerStatsManager.Instance.weaponDamageLevel >= PlayerStatsManager.Instance.weaponDamageMaxLevel;
            case Upgrade.WeaponCadence:
                return PlayerStatsManager.Instance.weaponCadenceLevel >= PlayerStatsManager.Instance.weaponCadenceMaxLevel;
        }

        return false;
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.gameState != GameState.GameRunning || isMaxLeveled()) return;

        int upgradeCost = GetUpgradeCost();
        if (PlayerStatsManager.Instance.totalCoin < upgradeCost) return;

        PlayerStatsManager.Instance.totalCoin -= upgradeCost;

        switch (this.upgradeType)
        {
            case Upgrade.ShieldQuant:
                PlayerStatsManager.Instance.LevelUpShieldQuantLevel();
                break;
            case Upgrade.ShieldSpeed:
                PlayerStatsManager.Instance.shieldSpeedLevel++;
                break;
            case Upgrade.WeaponQuant:
                PlayerStatsManager.Instance.LevelUpWeaponQuantLevel();
                break;
            case Upgrade.WeaponDamage:
                PlayerStatsManager.Instance.weaponDamageLevel++;
                break;
            case Upgrade.WeaponCadence:
                PlayerStatsManager.Instance.weaponCadenceLevel++;
                break;
        }
    }
}
