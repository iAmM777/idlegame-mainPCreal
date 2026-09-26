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
    [SerializeField] private TMP_Text BeansPowerClickText;


    public BigDouble BeansPower() => 1 + gameData.clickUpgradeLevel;
    private void Start()
    {
        gameData = new gameData();
        UpgradeManager.instance.StartUpgradeManager();
    }
    private void Update()
    {
        BeanText.text = gameData.Beans.ToString("F0") + " Beans!";
        BeansPowerClickText.text = "+" + BeansPower().ToString("F0") + " Beans";
    }

    public void AddBeans()
    {
        gameData.Beans += BeansPower();
    }
}
