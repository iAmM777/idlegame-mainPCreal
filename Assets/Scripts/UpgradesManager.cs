using BreakInfinity;
using NUnit.Framework;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Mono.Cecil;

public class UpgradeManager : MonoBehaviour 
{
    public static UpgradeManager instance;
    private void Awake() => instance = this;

    public List<Upgrades> clickUpgrades;
    public Upgrades clickUpgradePrefab;

    public ScrollRect clickUpgradesScroll;
    public Transform clickUpgradesPanel;

    public string[] clickUpgradeNames;



    public BigDouble[] clickUpgradeBaseCost;
    public BigDouble[] clickUpgradeCostMult;
    public BigDouble[] clickUpgradeBasePower;

    public void StartUpgradeManager()
    {
        clickUpgradeNames = new [] { "Bean Power +1", "Bean Power +5", "Bean Power +10" };
        clickUpgradeBaseCost = new BigDouble[] { 10, 50, 100 };
        clickUpgradeCostMult = new BigDouble[] { 1.25, 1.35, 1.55 };
        clickUpgradeBasePower = new BigDouble[] { 1, 5, 10 };

        for (int i = 0; i < Controller.instance.gameData.clickUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel);
            upgrade.upgradeID = i;
            clickUpgrades.Add(upgrade);
        }
        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);
        UpdateClickUpgradeUI();

    }

    public void UpdateClickUpgradeUI(int upgradeID = -1)
    {
        var data = Controller.instance.gameData;

        if (upgradeID == -1)
            for (int i = 0; i < clickUpgrades.Count; i++) UpdateUI(i);
        else UpdateUI(upgradeID);


        void UpdateUI(int ID)
        {
            clickUpgrades[ID].LevelText.text = data.clickUpgradeLevel[ID].ToString();
            clickUpgrades[ID].CostText.text = $"Cost:  {ClickUpgradeCost(ID).ToString("F0")}  Beans";
            clickUpgrades[ID].NameText.text = clickUpgradeNames[ID];
        }
    }
    public BigDouble ClickUpgradeCost(int UpgradeID) => clickUpgradeBaseCost[UpgradeID] * BigDouble.Pow(clickUpgradeCostMult[UpgradeID], Controller.instance.gameData.clickUpgradeLevel[UpgradeID]);

    public void BuyUpgrade(int UpgradeID)
    {
        var data = Controller.instance.gameData;
        if (data.Beans >= ClickUpgradeCost(UpgradeID))
        {
            data.Beans -= ClickUpgradeCost(UpgradeID);
            data.clickUpgradeLevel[UpgradeID]++;
        }

        UpdateClickUpgradeUI(UpgradeID);
    }
}
