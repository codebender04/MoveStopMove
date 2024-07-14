using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType 
{ 
    hammer = 0, 
    knife = 1, 
    boomerang = 2 
}
[CreateAssetMenu(menuName = "WeaponArraySO")]
public class WeaponArraySO : ScriptableObject
{
    [SerializeField] private Weapon[] weaponArray;
    public Weapon GetWeapon(WeaponType weaponType)
    {
        return weaponArray[(int)weaponType];
    }
}
