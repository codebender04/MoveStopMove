using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static void SaveJsonData(IEnumerable<ISaveable> saveables)
    {
        SaveData saveData = new SaveData();
        foreach (ISaveable saveable in saveables)
        {
            saveable.PopulateSaveData(saveData);
        }
        if (FileManager.WriteToFile("SaveData.dat", saveData.ToJson()))
        {
            Debug.Log("Save Succesful");
        }
    }
    public static void LoadJsonData(IEnumerable<ISaveable> saveables)
    {
        if (FileManager.LoadFromFile("SaveData.dat", out string json))
        {
            SaveData saveData = new SaveData();
            saveData.FromJson(json);
            foreach (ISaveable saveable in saveables)
            {
                saveable.LoadFromSaveData(saveData);
            }
            Debug.Log("Load Succesful");
        }
        
    }
}
