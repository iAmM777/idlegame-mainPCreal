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


    public List<Upgrades> productionUpgrades;
    public Upgrades ProductionUpgradesPrefab;

    public ScrollRect clickUpgradesScroll;
    public Transform clickUpgradesPanel;

    public ScrollRect productionUpgradesScroll;
    public Transform ProductionUpgradesPanel;

    public string[] clickUpgradeNames;
    public string[] productionUpgradeNames;


    public BigDouble[] clickUpgradeBaseCost;
    public BigDouble[] clickUpgradeCostMult;
    public BigDouble[] clickUpgradeBasePower;
    public BigDouble[] clickUpgradesUnlock;

    public BigDouble[] productionUpgradeBaseCost;
    public BigDouble[] productionUpgradeCostMult;
    public BigDouble[] productionUpgradeBasePower;
    public BigDouble[] productionUpgradesUnlock;


    public void StartUpgradeManager()
    {
        Methods.UpgradeCheck(Controller.instance.gameData.clickUpgradeLevel, 4);
        Methods.UpgradeCheck(Controller.instance.gameData.productionUpgradeLevel, 4);

        // Upgrade Names
        clickUpgradeNames = new [] { "Bean Power +1", "Bean Power +5", "Bean Power +10", "Bean Power +25" };
        productionUpgradeNames = new[] { "+1 Bean/s", "+2 Beans/s", "+5 Beans/s", "+10 Beans/s" };

        // Click Upgrades
        clickUpgradeBaseCost = new BigDouble[] { 10, 50, 100, 250 };
        clickUpgradeCostMult = new BigDouble[] { 1.25, 1.35, 1.55, 1.75 };
        clickUpgradeBasePower = new BigDouble[] { 1, 5, 10, 25 };
        clickUpgradesUnlock = new BigDouble[] { 0, 25, 50, 125 }; //half of clickUpgradeBaseCost

        //production upgrades
        productionUpgradeBaseCost = new BigDouble[] { 25, 50, 100, 250 };
        productionUpgradeCostMult = new BigDouble[] { 1.25, 1.35, 1.55, 1.75 };
        productionUpgradeBasePower = new BigDouble[] { 1, 5, 10, 25 };
        productionUpgradesUnlock = new BigDouble[] { 0, 25, 50, 125 }; //half of productionUpgradeBaseCost

        for (int i = 0; i < Controller.instance.gameData.clickUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel);
            upgrade.upgradeID = i;
            upgrade.gameObject.SetActive(false);
            clickUpgrades.Add(upgrade);
        }

        for (int i = 0; i < Controller.instance.gameData.productionUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(ProductionUpgradesPrefab, ProductionUpgradesPanel);
            upgrade.upgradeID = i;
            upgrade.gameObject.SetActive(false);
            productionUpgrades.Add(upgrade);
        }

        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);
        productionUpgradesScroll.normalizedPosition = new Vector2(0, 0);

        UpdateUpgradeUI("click");
        UpdateUpgradeUI("production");
    }


    public void Update()
    {
        for (var i = 0; i < clickUpgrades.Count; i++)
        {
            if (!clickUpgrades[i].gameObject.activeSelf)
                clickUpgrades[i].gameObject.SetActive(Controller.instance.gameData.Beans >= clickUpgradesUnlock[i]);
        }
        for (var i = 0; i < productionUpgrades.Count; i++)
        {
            if (!productionUpgrades[i].gameObject.activeSelf)
                productionUpgrades[i].gameObject.SetActive(Controller.instance.gameData.Beans >= productionUpgradesUnlock[i]);

        }
    }
    public void UpdateUpgradeUI(string type, int upgradeID = -1)
    {
        var data = Controller.instance.gameData;

        switch (type)
        {
            case "click":
                if (upgradeID == -1)
                    for (int i = 0; i < clickUpgrades.Count; i++) UpdateUI(clickUpgrades, data.clickUpgradeLevel, clickUpgradeNames, i);
                else UpdateUI(clickUpgrades, data.clickUpgradeLevel, clickUpgradeNames, upgradeID);
                break;
            case "production":
                if (upgradeID == -1)
                    for (int i = 0; i < productionUpgrades.Count; i++) UpdateUI(productionUpgrades, data.productionUpgradeLevel, productionUpgradeNames, i);
                else UpdateUI(productionUpgrades, data.productionUpgradeLevel, productionUpgradeNames, upgradeID);
                break;
        }

        void UpdateUI(List<Upgrades> upgrades, List<int> upgradeLevels, string[] upgradeNames, int ID)
        {
            upgrades[ID].LevelText.text = upgradeLevels[ID].ToString();
            upgrades[ID].CostText.text = $"Cost:  {UpgradeCost(type, ID):F2}  Beans";
            upgrades[ID].NameText.text = upgradeNames[ID];
        }
    }
    public BigDouble UpgradeCost(string type, int UpgradeID)
    {
        var data = Controller.instance.gameData;
        switch (type) 
        { 
            case "click":
                return clickUpgradeBaseCost[UpgradeID] * BigDouble.Pow(clickUpgradeCostMult[UpgradeID], data.clickUpgradeLevel[UpgradeID]);

            case "production":
                return productionUpgradeBaseCost[UpgradeID] * BigDouble.Pow(productionUpgradeCostMult[UpgradeID], data.productionUpgradeLevel[UpgradeID]);
        }
        return 0;
    }

    public void BuyUpgrade(string type, int UpgradeID)
    {
        var data = Controller.instance.gameData;

        switch (type)
        {
            case "click":
                Buy(data.clickUpgradeLevel);
                break;
            case "production":
                Buy(data.productionUpgradeLevel);
                break;
        }

        void Buy(List<int> upgradeLevels)
        {
            if (data.Beans >= UpgradeCost(type, UpgradeID))
            {
                data.Beans -= UpgradeCost(type, UpgradeID);
                upgradeLevels[UpgradeID]++;
            }

            UpdateUpgradeUI(type, UpgradeID);
        }
    }
}
