using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Navigation : MonoBehaviour
{
    public GameObject ClickUpgradeSelected;
    public GameObject ProductionUpgradesSelected;

    public TMP_Text ClickUpgradeTitleText;
    public TMP_Text ProductionUpgradeTitleText;

    public void SwitchUpgrades(string location)
    {
        UpgradeManager.instance.clickUpgradesScroll.gameObject.SetActive(false);
        UpgradeManager.instance.productionUpgradesScroll.gameObject.SetActive(false);

        ClickUpgradeSelected.SetActive(false);
        ProductionUpgradesSelected.SetActive(false);

        ClickUpgradeTitleText.color = Color.gray;
        ProductionUpgradeTitleText.color = Color.gray;

        switch (location)
        {
            case "Click":
                UpgradeManager.instance.clickUpgradesScroll.gameObject.SetActive(true);
                ClickUpgradeSelected.SetActive(true);
                ClickUpgradeTitleText.color = Color.white;
                break;
            case "Production":
                UpgradeManager.instance.productionUpgradesScroll.gameObject.SetActive(true);
                ProductionUpgradesSelected.SetActive(true);
                ProductionUpgradeTitleText.color = Color.white;
                break;
        }
    }
}
