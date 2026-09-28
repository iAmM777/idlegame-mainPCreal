using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using File = UnityEngine.Windows.File;
using TMPro;
using UnityEngine.UI;
using System.Reflection.PortableExecutable;
public class SaveSystem : MonoBehaviour
{
    public TMP_InputField ImportField;
    public TMP_InputField ExportField;

    public Image CopyButton;
    public Image Pastebutton;

    public TMP_Text CopyButtonText;
    public TMP_Text PasteButtonText;


    private const string FileType = ".txt";
    private const string FilePath = "PlayerData_Tutorial";
    private static string SavePath => Application.persistentDataPath + "/Saves/";
    private static string BackUpSavePath => Application.persistentDataPath + "/BackUps/";

    private static int SaveCount;

    public static void SaveData<T>(T gameData, string fileName)
    {
        Directory.CreateDirectory(SavePath);
        Directory.CreateDirectory(BackUpSavePath);

        if(SaveCount % 5 == 0) Save(BackUpSavePath);
        Save(SavePath);


        void Save(string path)
        {
            using (StreamWriter writer = new StreamWriter(path + fileName + FileType))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream memoryStream = new MemoryStream();
                formatter.Serialize(memoryStream, gameData);
                string dataToSave = Convert.ToBase64String(memoryStream.ToArray());
                writer.Write(dataToSave);
            }
        }
    }
    public static T LoadData<T>(string fileName)
    {
        Directory.CreateDirectory(SavePath);
        Directory.CreateDirectory(BackUpSavePath);

        //check later if broken
        bool backUpNeeded = false;
        T dataToReturn = default;

        Load(SavePath);
        if (backUpNeeded) Load(BackUpSavePath);

        return dataToReturn;

        void Load(string path)
        {
            using (StreamReader reader = new StreamReader(path + fileName + FileType))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                string dataToLoad = reader.ReadToEnd();
                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(dataToLoad));

                try
                {
                    dataToReturn = (T)formatter.Deserialize(memoryStream);
                }
                catch
                {
                    backUpNeeded = true;
                    dataToReturn = default;
                }
            }
        }
    }

    public static bool SaveExists(string fileName)
    {
        if (File.Exists(SavePath + fileName + FileType))
            return true;
        else if (File.Exists(BackUpSavePath + fileName + FileType))
            return true;
        else
            return false;
    }


    public void Import()
    { 
        Directory.CreateDirectory(SavePath);

        using (StreamWriter writer = new StreamWriter($"{SavePath}{FilePath}{FileType}"))
        {
            writer.WriteLine(ImportField.text);
            writer.Close();
        }

        Controller.instance.gameData = SaveExists(FilePath)
            ? LoadData<gameData>(FilePath)
            : new gameData();
    }

    public void Export()
    {
        Controller.instance.Save();
        Directory.CreateDirectory(SavePath);

        using (StreamReader reader = new StreamReader($"{SavePath}{FilePath}{FileType}"))
        {
            ExportField.text = reader.ReadToEnd();
            reader.Close();
        }
    }
    public void Copy() 
    {
        if (ExportField.text == "") return;
        GUIUtility.systemCopyBuffer = ExportField.text;
        CopyButton.color = Color.green;
        CopyButtonText.text = "Copied!";
        StartCoroutine(CopyPasteButtonsNormal());
    }
    public void Paste()
    {
        ImportField.text = GUIUtility.systemCopyBuffer;
        PasteButtonText.color = Color.green;
        PasteButtonText.text = "Pasted!";
        StartCoroutine(CopyPasteButtonsNormal());
    }
    public void Clear(string type)
    {
        if (type == "Export")
        {
            ExportField.text = "";
            return;
        }
        ImportField.text = "";
    }

    public IEnumerator CopyPasteButtonsNormal()
    {
        yield return new WaitForSeconds(2f);
        CopyButton.color = Color.white; //subject to change
        CopyButtonText.text = "Copy to Clipboard";
        if (Pastebutton != null) Pastebutton.color = Color.white; //subject to change       
        PasteButtonText.text = "Paste Clipboard";
    }
}
