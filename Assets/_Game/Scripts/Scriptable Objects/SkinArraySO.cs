using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pants
{
    Batman = 0,
    Chambi = 1,
    Comy = 2,
    Dabao = 3,
    Onion = 4,
    Pokemon = 5,
    Rainbow = 6,
    Skull = 7,
    Vantim = 8,
}
public enum Hat
{
    None = 0,
    Arrow = 1,
    Cowboy = 2,
    Crown = 3,
    Ear = 4,
    Hat = 5,
    Hatcap = 6,
    HatYellow = 7,
    Headphone = 8,
    Horn = 9,
    Rau = 10,
}
[CreateAssetMenu(menuName = "SkinArraySO")]
public class SkinArraySO : ScriptableObject
{
    [SerializeField] private Material[] pantsMaterial;
    [SerializeField] private GameObject[] hatModels;
    public Material GetPants(Pants pants)
    {
        return pantsMaterial[(int)pants];
    }
    public GameObject GetHat(Hat hat)
    {
        return hatModels[(int)hat];
    }
}
