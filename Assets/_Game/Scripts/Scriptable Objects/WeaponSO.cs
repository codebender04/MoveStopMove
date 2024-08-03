using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public float range;
    public float speed;
    public Material[] skins;
    public int price;
}
