using UnityEngine;
using TMPro;
using Unity.Android.Gradle.Manifest;
//using UnityEngine.UI;
//[System.Serializable]

public class Controller : MonoBehaviour
{
    public gameData gameData;
    [SerializeField] private TMP_Text BeanText;


    private void Start()
    {
        gameData = new gameData();
    }
    private void Update()
    {
        BeanText.text = gameData.Beans + " Beans!";
    }

    public void AddBeans()
    {
        gameData.Beans++;
    }
}
