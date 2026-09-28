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

    public UpgradeHandler[] upgradeHandlers;


    public void StartUpgradeManager()
    {
        Methods.UpgradeCheck(Controller.instance.gameData.clickUpgradeLevel, 4);
        Methods.UpgradeCheck(Controller.instance.gameData.productionUpgradeLevel, 4);
        Methods.UpgradeCheck(Controller.instance.gameData.generatorUpgradeLevel, 4);

        



        // Upgrade Names
        upgradeHandlers[0].UpgradeNames = new [] { "Bean Power +1", "Bean Power +5", "Bean Power +10", "Bean Power +25" };
        upgradeHandlers[1].UpgradeNames = new[] { "+1 Bean/s", "+2 Beans/s", "+5 Beans/s", "+10 Beans/s" };
        upgradeHandlers[2].UpgradeNames = new[] 
        { 
            $"Produces +0.1 \"{upgradeHandlers[1].UpgradeNames[0]}\" Upgrades/s",
            $"Produces +0.05 \"{upgradeHandlers[1].UpgradeNames[1]}\" Upgrades/s",
            $"Produces +0.02 \"{upgradeHandlers[1].UpgradeNames[2]}\" Upgrades/s",
            $"Produces +1.01 \"{upgradeHandlers[1].UpgradeNames[3]}\" Upgrades/s"
        };

        // Click Upgrades
        upgradeHandlers[0].UpgradeBaseCost = new BigDouble[] { 10, 50, 100, 250 };
        upgradeHandlers[0].UpgradeCostMult = new BigDouble[] { 1.25, 1.35, 1.55, 1.75 };
        upgradeHandlers[0].UpgradeBasePower = new BigDouble[] { 1, 5, 10, 25 };
        upgradeHandlers[0].UpgradesUnlock = new BigDouble[] { 0, 25, 50, 125 }; //half of clickUpgradeBaseCost

        //production upgrades
        upgradeHandlers[1].UpgradeBaseCost = new BigDouble[] { 25, 50, 100, 250 };
        upgradeHandlers[1].UpgradeCostMult = new BigDouble[] { 1.25, 1.35, 1.55, 1.75 };
        upgradeHandlers[1].UpgradeBasePower = new BigDouble[] { 1, 5, 10, 25 };
        upgradeHandlers[1].UpgradesUnlock = new BigDouble[] { 0, 25, 50, 125 }; //half of productionUpgradeBaseCost\

        //generator upgrades
        upgradeHandlers[2].UpgradeBaseCost = new BigDouble[] { 5000, 1e4, 1e5, 1e6 };
        upgradeHandlers[2].UpgradeCostMult = new BigDouble[] { 1.25, 1.5, 2, 2.5 };
        upgradeHandlers[2].UpgradeBasePower = new BigDouble[] { 0.1, 0.05, 0.02, 0.01 };
        upgradeHandlers[2].UpgradesUnlock = new BigDouble[] { 2500, 5e3, 5e4, 5e5 }; //half of productionUpgradeBaseCost

        CreateUpgrades(Controller.instance.gameData.clickUpgradeLevel, 0);
        CreateUpgrades(Controller.instance.gameData.productionUpgradeLevel, 1);
        CreateUpgrades(Controller.instance.gameData.generatorUpgradeLevel, 2);

        void CreateUpgrades<T>(List<T> level, int index)
        {
            for (int i = 0; i < level.Count; i++)
            {
                Upgrades upgrade = Instantiate(upgradeHandlers[index].UpgradePrefab, upgradeHandlers[index].UpgradesPanel);
                upgrade.upgradeID = i;
                upgrade.gameObject.SetActive(false);
                upgradeHandlers[index].Upgrades.Add(upgrade);
            }
            upgradeHandlers[index].UpgradesScroll.normalizedPosition = new Vector2(0, 0);
        }
        UpdateUpgradeUI("click");
        UpdateUpgradeUI("production");
        UpdateUpgradeUI("generator");
    }


    public void Update()
    {

        UpgradeUnlockSystem(Controller.instance.gameData.Beans, upgradeHandlers[0].UpgradesUnlock, 0);
        UpgradeUnlockSystem(Controller.instance.gameData.Beans, upgradeHandlers[1].UpgradesUnlock, 1);
        UpgradeUnlockSystem(Controller.instance.gameData.Beans, upgradeHandlers[2].UpgradesUnlock, 2);

        void UpgradeUnlockSystem(BigDouble currency, BigDouble[] unlock, int index)
        {
            for (var i = 0; i < upgradeHandlers[index].Upgrades.Count; i++)
            {
                if (!upgradeHandlers[index].Upgrades[i].gameObject.activeSelf)
                    upgradeHandlers[index].Upgrades[i].gameObject.SetActive(currency >= unlock[i]);
            }
        }
    }
    public void UpdateUpgradeUI(string type, int upgradeID = -1)
    {
        var data = Controller.instance.gameData;

        switch (type)
        {
            case "click":
                if (upgradeID == -1)
                    UpdateAllUI(upgradeHandlers[0].Upgrades, data.clickUpgradeLevel, upgradeHandlers[0].UpgradeNames, 0);
                break;
            case "production":
                UpdateAllUI(upgradeHandlers[1].Upgrades, data.productionUpgradeLevel, upgradeHandlers[1].UpgradeNames, 1);
                break;
            case "generators":
                UpdateAllUI(upgradeHandlers[2].Upgrades, data.generatorUpgradeLevel, upgradeHandlers[2].UpgradeNames, 2);
                break;
        }

        void UpdateAllUI<T>(List<Upgrades> upgrades, List<T> upgradeLevels, string[] upgradeNames, int index)
        {
            if (upgradeID == -1)
                for (int i = 0; i < upgradeHandlers[index].Upgrades.Count; i++) 
                    UpdateUI(i);
            else UpdateUI(upgradeID);

            void UpdateUI(int ID) 
            {
                upgrades[ID].LevelText.text = upgradeLevels[ID].ToString();
                upgrades[ID].CostText.text = $"Cost:  {UpgradeCost(type, ID):F2}  Beans";
                upgrades[ID].NameText.text = upgradeNames[ID];
            }
        }
    }
    public BigDouble UpgradeCost(string type, int UpgradeID)
    {
        var data = Controller.instance.gameData;
        switch (type)
        {
            case "click":
                return UpgradeCost_int(0, data.clickUpgradeLevel, UpgradeID);
            case "production":
                return UpgradeCost_BigDouble(1, data.productionUpgradeLevel, UpgradeID);
            case "generators":
                return UpgradeCost_BigDouble(2, data.generatorUpgradeLevel, UpgradeID);
        }
        return 0;
    }
    private BigDouble UpgradeCost_BigDouble(int index, List<BigDouble> levels, int UpgradeID)
    {
        return upgradeHandlers[index].UpgradeBaseCost[UpgradeID] 
                * BigDouble.Pow(upgradeHandlers[index].UpgradeCostMult[UpgradeID], 
                    (BigDouble)Controller.instance.gameData.clickUpgradeLevel[UpgradeID]);
    }
    private BigDouble UpgradeCost_int(int index, List<int> levels, int UpgradeID)
    {
        return upgradeHandlers[index].UpgradeBaseCost[UpgradeID]
                * BigDouble.Pow(upgradeHandlers[index].UpgradeCostMult[UpgradeID],
                    (BigDouble)Controller.instance.gameData.clickUpgradeLevel[UpgradeID]);
    }
    

    public void BuyUpgrade(string type, int UpgradeID)
    {
        var data = Controller.instance.gameData;

        switch (type)
        {
            case "click":
                Buy(data.clickUpgradeLevel, type, UpgradeID);
                break;
            case "production":
                Buy(data.productionUpgradeLevel, type, UpgradeID);
                break;
            case "generators":
                Buy(data.generatorUpgradeLevel, type, UpgradeID);
                break;
        }
    }
    private void Buy(List<int> upgradeLevels, string type, int UpgradeID)
    {
        var data = Controller.instance.gameData;
        if (data.Beans >= UpgradeCost(type, UpgradeID))
        {
            data.Beans -= UpgradeCost(type, UpgradeID);
            upgradeLevels[UpgradeID]++;
        }

        UpdateUpgradeUI(type, UpgradeID);
    }

    private void Buy(List<BigDouble> upgradeLevels, string type, int UpgradeID)
    {
        var data = Controller.instance.gameData;
        if (data.Beans >= UpgradeCost(type, UpgradeID))
        {
            data.Beans -= UpgradeCost(type, UpgradeID);
            upgradeLevels[UpgradeID]++;
        }

        UpdateUpgradeUI(type, UpgradeID);
    }
}
