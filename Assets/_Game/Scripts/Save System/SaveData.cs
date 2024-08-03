using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveData
{
    public int gold;
    public int currentWeaponIndex;
    public int currentSkinIndex;
    public bool[] weaponsPurchased;
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
    public void FromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}
public interface ISaveable
{
    public void PopulateSaveData(SaveData saveData);
    public void LoadFromSaveData(SaveData saveData);
}
