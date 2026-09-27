using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Navigation : MonoBehaviour
{
    public GameObject ClickUpgradeSelected;
    public GameObject ProductionUpgradesSelected;
    public GameObject GeneratorUpgradesSelected;

    public TMP_Text ClickUpgradeTitleText;
    public TMP_Text ProductionUpgradeTitleText;
    public TMP_Text GeneratorUpgradesTitleText;

    public void SwitchUpgrades(string location)
    {
        UpgradeManager.instance.upgradeHandlers[0].UpgradesScroll.gameObject.SetActive(false);
        UpgradeManager.instance.upgradeHandlers[1].UpgradesScroll.gameObject.SetActive(false);
        UpgradeManager.instance.upgradeHandlers[2].UpgradesScroll.gameObject.SetActive(false);

        ClickUpgradeSelected.SetActive(false);
        ProductionUpgradesSelected.SetActive(false);
        GeneratorUpgradesSelected.SetActive(false);

        ClickUpgradeTitleText.color = Color.gray;
        ProductionUpgradeTitleText.color = Color.gray;
        GeneratorUpgradesTitleText.color = Color.gray;

        switch (location)
        {
            case "Click":
                UpgradeManager.instance.upgradeHandlers[0].UpgradesScroll.gameObject.SetActive(true);
                ClickUpgradeSelected.SetActive(true);
                ClickUpgradeTitleText.color = Color.white;
                break;
            case "Production":
                UpgradeManager.instance.upgradeHandlers[1].UpgradesScroll.gameObject.SetActive(true);
                ProductionUpgradesSelected.SetActive(true);
                ProductionUpgradeTitleText.color = Color.white;
                break;
            case "Generator":
                UpgradeManager.instance.upgradeHandlers[2].UpgradesScroll.gameObject.SetActive(true);
                GeneratorUpgradesSelected.SetActive(true);
                GeneratorUpgradesTitleText.color = Color.white;
                break;
        }
    }
}
