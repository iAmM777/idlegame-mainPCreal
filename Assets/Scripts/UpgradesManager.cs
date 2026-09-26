using BreakInfinity;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class UpgradeManager : MonoBehaviour 
{
    public static UpgradeManager instance;
    private void Awake() => instance = this;
    public Upgrades clickUpgrade;
    public string clickUpgradeName;



    public BigDouble clickUpgradeBaseCost;
    public BigDouble clickUpgradeCostMult;

    public void StartUpgradeManager()
    {
        Controller.instance.gameData = new gameData();
        clickUpgradeName = "Beans Per Click";
        clickUpgradeBaseCost = 10;
        clickUpgradeCostMult = 1.25;
        UpdateClickUpgradeUI();

    }

    public void UpdateClickUpgradeUI()
    {
        clickUpgrade.LevelText.text = Controller.instance.gameData.clickUpgradeLevel.ToString();
        clickUpgrade.CostText.text = "Cost: " + Cost().ToString("F0") + " Beans";
        clickUpgrade.NameText.text = "+1 " + clickUpgradeName;
    }
    public BigDouble Cost() => clickUpgradeBaseCost * BigDouble.Pow(clickUpgradeCostMult, Controller.instance.gameData.clickUpgradeLevel);

    public void BuyUpgrade()
    {
        var data = Controller.instance.gameData;
        if (data.Beans >= Cost())
        {
            data.Beans -= Cost();
            data.clickUpgradeLevel++;
        }

        UpdateClickUpgradeUI();
    }
}
