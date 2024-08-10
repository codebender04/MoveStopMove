using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveData
{
    public int gold;
    public int currentWeaponIndex;
    public int currentSkinIndex;
    public int currentHatIndex;
    public int currentPantsIndex;
    public bool[] weaponsPurchased;
    public bool[] hatsPurchased;
    public bool[] pantsPurchased;
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
