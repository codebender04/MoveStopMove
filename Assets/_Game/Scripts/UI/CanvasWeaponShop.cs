using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWeaponShop : UICanvas
{
    [SerializeField] private List<WeaponSO> weaponSOList;
    [SerializeField] private Transform handTransform;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private TextMeshProUGUI selectText;
    [SerializeField] private GameObject[] shopWeaponModels;
    [SerializeField] private GameObject[] playerWeaponModels;
    [SerializeField] private GameObject customizeUI;
    private Player player;
    private int currentWeaponIndex = 0;
    private bool[] WeaponsPurchased => player.WeaponsPurchased;
    private int selectedWeaponIndex;
    private int selectedSkinIndex;
    private int[] skinIndexArray = new int[3] {0, 0, 0};
    private void OnEnable()
    {
        if (player == null) player = FindObjectOfType<Player>();

        currentWeaponIndex = (int)player.GetWeaponType();
        selectedWeaponIndex = currentWeaponIndex;
        selectedSkinIndex = player.GetCurrentSkinIndex();
        SelectSkinButton(selectedSkinIndex);
    }
    public void ShowPreviousWeapon()
    {
        currentWeaponIndex--;
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = shopWeaponModels.Length - 1;
        }
        UpdateWeaponUI();
    }
    public void ShowNextWeapon() 
    {
        currentWeaponIndex++;
        if (currentWeaponIndex > shopWeaponModels.Length - 1)
        {
            currentWeaponIndex = 0;
        }
        UpdateWeaponUI();
    }
    private void UpdateWeaponUI()
    {
        for (int i = 0; i < shopWeaponModels.Length; i++)
        {
            shopWeaponModels[i].SetActive(i == currentWeaponIndex);
            MeshRenderer meshRenderer = shopWeaponModels[i].GetComponent<MeshRenderer>();
            SkinManager.SetSkin(meshRenderer, weaponSOList[currentWeaponIndex].skins[skinIndexArray[currentWeaponIndex]]);
        }
        customizeUI.SetActive(false);

        weaponNameText.text = weaponSOList[currentWeaponIndex].name.ToUpper();
        priceText.text = weaponSOList[currentWeaponIndex].price.ToString();

        buyButton.gameObject.SetActive(!WeaponsPurchased[currentWeaponIndex]);
        selectButton.gameObject.SetActive(WeaponsPurchased[currentWeaponIndex]);

        selectText.text = selectedWeaponIndex == currentWeaponIndex && selectedSkinIndex == skinIndexArray[currentWeaponIndex] ? "Equipped" : "Select";
        
        SetPlayerWeaponModel();

        buyButton.interactable = player.GetGold() >= weaponSOList[currentWeaponIndex].price;
    }
    public void CustomSkinButton()
    {
        UpdateWeaponUI();
        customizeUI.SetActive(true);
    }
    public void BuyButton()
    {
        player.UpdateGold(-weaponSOList[currentWeaponIndex].price);
        WeaponsPurchased[currentWeaponIndex] = true;
        UpdateWeaponUI();
    }
    public void SelectButton()
    {
        player.SetWeapon((WeaponType)currentWeaponIndex);
        player.SetWeaponSkin(skinIndexArray[currentWeaponIndex]);
        selectedWeaponIndex = currentWeaponIndex;
        selectedSkinIndex = skinIndexArray[currentWeaponIndex];

        UpdateWeaponUI();
    }
    public void SelectSkinButton(int skinIndex)
    {
        skinIndexArray[currentWeaponIndex] = skinIndex;
        
        UpdateWeaponUI();
    }
    private void SetPlayerWeaponModel()
    {
        for (int i = 0; i < playerWeaponModels.Length; i++)
        {
            if (i == currentWeaponIndex)
            {
                playerWeaponModels[i].SetActive(true);
                SkinManager.SetSkin(playerWeaponModels[i].GetComponent<MeshRenderer>(), weaponSOList[currentWeaponIndex].skins[skinIndexArray[currentWeaponIndex]]);
            }
            else
            {
                playerWeaponModels[i].SetActive(false);
            }
        }
    }
}
