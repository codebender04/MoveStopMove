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
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private TextMeshProUGUI selectText;
    [SerializeField] private GameObject[] shopWeaponModels;
    [SerializeField] private GameObject[] playerWeaponModels;
    [SerializeField] private GameObject[] colorablePartsButtons;
    [SerializeField] private GameObject customizeUI;
    [SerializeField] private Image[] skinButtonImage;
    private Player player;
    private int currentWeaponIndex = 0;
    private bool[] WeaponsPurchased => player.WeaponsPurchased;
    private int[] skinIndexArray = new int[3] {0, 0, 0};
    private int selectedWeaponIndex;
    private int selectedSkinIndex;
    private int colorablePartIndex = 0;
    private void OnEnable()
    {
        if (player == null) player = FindObjectOfType<Player>();

        goldText.text = player.GetGold().ToString();
        player.OnGoldChanged += Player_OnGoldChanged;

        currentWeaponIndex = (int)player.GetWeaponType();
        selectedWeaponIndex = currentWeaponIndex;
        selectedSkinIndex = player.GetCurrentSkinIndex();
        SelectSkinButton(selectedSkinIndex);
    }

    private void Player_OnGoldChanged(object sender, Player.OnGoldChangedEventArgs e)
    {
        goldText.text = e.gold.ToString();
    }

    public void ShowPreviousWeapon()
    {
        currentWeaponIndex--;
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = shopWeaponModels.Length - 1;
        }
        UpdateWeaponShopUI();
    }
    public void ShowNextWeapon() 
    {
        currentWeaponIndex++;
        if (currentWeaponIndex > shopWeaponModels.Length - 1)
        {
            currentWeaponIndex = 0;
        }
        UpdateWeaponShopUI();
    }
    private void UpdateWeaponShopUI()
    {
        for (int i = 0; i < shopWeaponModels.Length; i++)
        {
            shopWeaponModels[i].SetActive(i == currentWeaponIndex);
            MeshRenderer meshRenderer = shopWeaponModels[i].GetComponent<MeshRenderer>();
            SkinManager.SetSkin(meshRenderer, weaponSOList[currentWeaponIndex].skins[skinIndexArray[currentWeaponIndex]]);
        }
        customizeUI.SetActive(false);
        skinButtonImage[0].sprite = weaponSOList[currentWeaponIndex].skinIcons[0];
        skinButtonImage[1].sprite = weaponSOList[currentWeaponIndex].skinIcons[1];

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
        UpdateWeaponShopUI();
        customizeUI.SetActive(true);
        for (int i = 0; i < colorablePartsButtons.Length; i++)
        {
            colorablePartsButtons[i].SetActive(i < weaponSOList[currentWeaponIndex].colorableParts);
        }
    }
    public void SelectColorWeapon(int colorIndex)
    {
        
    }
    public void SelectColorablePart(int colorablePartIndex)
    {
        this.colorablePartIndex = colorablePartIndex;
    }
    public void BuyButton()
    {
        player.UpdateGold(-weaponSOList[currentWeaponIndex].price);
        WeaponsPurchased[currentWeaponIndex] = true;
        UpdateWeaponShopUI();
    }
    public void SelectButton()
    {
        player.SetWeapon((WeaponType)currentWeaponIndex);
        player.SetWeaponSkin(skinIndexArray[currentWeaponIndex]);
        selectedWeaponIndex = currentWeaponIndex;
        selectedSkinIndex = skinIndexArray[currentWeaponIndex];

        UpdateWeaponShopUI();
    }
    public void SelectSkinButton(int skinIndex)
    {
        skinIndexArray[currentWeaponIndex] = skinIndex;
        
        UpdateWeaponShopUI();
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
