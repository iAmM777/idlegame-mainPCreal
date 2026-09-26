using BreakInfinity;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class UpgradeManager : MonoBehaviour 
{
    public Controller controller;
    public Upgrades clickUpgrade;
    public string clickUpgradeName;



    public BigDouble clickUpgradeBaseCost;
    public BigDouble clickUpgradeCostMult;

    public void StartUpgradeManager()
    {
        clickUpgradeName = "Beans Per Click";
        clickUpgradeBaseCost = 10;
        clickUpgradeCostMult = 1.25;
        UpdateClickUpgradeUI();

    }

    public void UpdateClickUpgradeUI()
    {
        clickUpgrade.LevelText.text = controller.gameData.clickUpgradeLevel.ToString();
        clickUpgrade.CostText.text = "Cost: " + Cost().ToString("F0") + " Beans";
        clickUpgrade.NameText.text = "+1 " + clickUpgradeName;
    }
    public BigDouble Cost() => clickUpgradeBaseCost * BigDouble.Pow(clickUpgradeCostMult, controller.gameData.clickUpgradeLevel);

    public void BuyUpgrade()
    {
        if(controller.gameData.Beans >= Cost())
        {
            controller.gameData.Beans -= Cost();
            controller.gameData.clickUpgradeLevel++;
        }

        UpdateClickUpgradeUI();
    }
}
