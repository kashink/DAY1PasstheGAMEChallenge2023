using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsManager : MonoBehaviour
{
    [Space()]
    [Header("Stats")]
    [SerializeField] public int damage = 1;
    [SerializeField] public float shieldRotationSpeed = 50f;

    [Space()]
    [Header("Refs")]
    [SerializeField] public Transform planetTransform;
    [SerializeField] List<GameObject> shieldList;
    [SerializeField] List<GameObject> weaponList;

    [Space()]
    [Header("Coins")]
    [SerializeField] TextMeshProUGUI totalCoinText;
    [SerializeField] public int totalCoin = 0;

    [Space()]
    [Header("Life")]
    [SerializeField] int maxLife = 100;
    [SerializeField] GameObject lifeBarFill;
    [SerializeField] float lifeBarSize;

    int currentLife = 100;

    public int shieldQuantLevel = 1;
    public int shieldSpeedLevel = 1;
    public int weaponQuantLevel = 1;
    public int weaponDamageLevel = 1;
    public int weaponCadenceLevel = 1;

    public int shieldQuantMaxLevel = 8;
    public int shieldSpeedMaxLevel = 8;
    public int weaponQuantMaxLevel = 9;
    public int weaponDamageMaxLevel = 8;
    public int weaponCadenceMaxLevel = 8;

    public static PlayerStatsManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        this.currentLife = this.maxLife;
    }

    public void TakeDamage(int damage)
    {
        this.currentLife -= damage;
        if (this.currentLife < 0) this.currentLife = 0;
        UpdateLifeUI();

        if (this.currentLife > 0) return;

        GameManager.Instance.ChangeState(GameState.GameOver);
        return;
    }

    void UpdateLifeUI()
    {
        float healthPercentage = (float)this.currentLife / (float)this.maxLife;

        this.lifeBarFill.GetComponent<RectTransform>().sizeDelta = new Vector2(this.lifeBarSize * healthPercentage, this.lifeBarFill.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void ReceiveCoins(int addCoins)
    {
        this.totalCoin += addCoins;
        UpdateCoinsUI();
    }

    void UpdateCoinsUI()
    {
        this.totalCoinText.text = $"{this.totalCoin}";
    }

    public void LevelUpShieldQuantLevel()
    {
        if (this.shieldList.Count <= this.shieldQuantLevel) return;
        this.shieldList[this.shieldQuantLevel].SetActive(true);

        this.shieldQuantLevel++;
    }

    public void LevelUpWeaponQuantLevel()
    {
        int weaponIndex = this.weaponQuantLevel - 1;
        if (this.weaponList.Count <= weaponIndex) return;
        this.weaponList[weaponIndex].SetActive(true);

        this.weaponQuantLevel++;
    }
}
