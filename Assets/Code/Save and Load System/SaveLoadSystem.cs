using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoadSystem
{
    public static readonly string SAVE_FOLDER_NONEDITOR = Application.persistentDataPath + "/Saves/";

    public static void InitialiseData(List<LevelDataSO> levelDatas)
    {
        bool directoryExists;
        directoryExists = Directory.Exists(SAVE_FOLDER_NONEDITOR);

        if (!directoryExists)
        {
            Directory.CreateDirectory(SAVE_FOLDER_NONEDITOR);

            SaveData saveData = new SaveData(0.5f, 0.5f , levelDatas);
            string json = JsonUtility.ToJson(saveData);
            SaveData(json);
        }
    }



    public static void SaveData(string json)
    {
        File.WriteAllText(SAVE_FOLDER_NONEDITOR + "/Save.json", json);
    }

    public static string LoadData()
    {
        if (File.Exists(SAVE_FOLDER_NONEDITOR + "/Save.json"))
        {
            string savestring = File.ReadAllText(SAVE_FOLDER_NONEDITOR + "/Save.json");
            SaveData saveData = JsonUtility.FromJson<SaveData>(savestring);
            return savestring;
        }
        else
        {
            return null;
        }
    }
}
