using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Upgrades : MonoBehaviour
{
    public int upgradeID;
    public Image UpgradeButton;
    public TMP_Text LevelText;
    public TMP_Text NameText;
    public TMP_Text CostText;  



    public void BuyClickUpgrade() => UpgradeManager.instance.BuyUpgrade("click", upgradeID);
    public void BuyProductionUpgrade() => UpgradeManager.instance.BuyUpgrade("production", upgradeID);
}
