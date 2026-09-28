using UnityEngine;
using TMPro;
using Unity.Android.Gradle.Manifest;
using BreakInfinity;
//using UnityEngine.UI;
//[System.Serializable]

public class Controller : MonoBehaviour
{
    public static Controller instance;
    private void Awake() => instance = this;

    public gameData gameData;
    [SerializeField] private TMP_Text BeanText;
    [SerializeField] private TMP_Text BeansPerSecondText;
    [SerializeField] private TMP_Text BeansPowerClickText;


    public BigDouble BeansPower() 
    {
        BigDouble total = 1;
        if (UpgradeManager.instance?.upgradeHandlers?[0]?.UpgradeBasePower == null) return total;
        for (int i = 0; i < gameData.clickUpgradeLevel.Count; i++)
            total += UpgradeManager.instance.upgradeHandlers[0].UpgradeBasePower[i] * gameData.clickUpgradeLevel[i];
        return total;
       
    }

    public BigDouble BeansPerSecond() 
    {
        BigDouble total = 0;
        if (UpgradeManager.instance?.upgradeHandlers?[1]?.UpgradeBasePower == null) return total;
        for (int i = 0; i < gameData.productionUpgradeLevel.Count; i++)
            total += UpgradeManager.instance.upgradeHandlers[1].UpgradeBasePower[i] 
                * (gameData.productionUpgradeLevel[i] + gameData.productionUpgradeGenerated[i]);
        return total;
    }

    public BigDouble UpgradesPerSecond(int index)
    { 
        if (UpgradeManager.instance?.upgradeHandlers?[2]?.UpgradeBasePower == null) return 0;
        return UpgradeManager.instance.upgradeHandlers[2].UpgradeBasePower[index] * gameData.generatorUpgradeLevel[index];
    }

    private void Start()
    {
        gameData = new gameData();
        UpgradeManager.instance.StartUpgradeManager();
    }
    private void Update()
    {
        BeanText.text = gameData.Beans.ToString("F0") + " Beans!";
        BeansPowerClickText.text = "+" + BeansPower().ToString("F0") + " Beans";

        BeansPerSecondText.text = $"{BeansPerSecond():F2} Beans/s";
        gameData.Beans += BeansPerSecond() * Time.deltaTime;

        for (var i = 0; i < gameData.generatorUpgradeLevel.Count; i++)
        {
            gameData.productionUpgradeGenerated[i] += UpgradesPerSecond(i) * Time.deltaTime;
        }
    }

    public void AddBeans()
    {
        gameData.Beans += BeansPower();
    }
}
