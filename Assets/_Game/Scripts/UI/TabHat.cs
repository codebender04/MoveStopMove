using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabHat : Tab
{
    [SerializeField] private Transform headTransform;
    [SerializeField] private int hatPrice;
    private bool[] HatsPurchased => player.HatsPurchased;
    private GameObject currentHatModel;
    public override void UpdateTabUI()
    {
        SetPlayerModelHat((Hat)currentItemIndex);

        for (int i = 0; i < buttonObjectArray.Length; i++)
        {
            buttonObjectArray[i].lockImage.SetActive(!player.HatsPurchased[i]);
        }

        buyButton.gameObject.SetActive(!HatsPurchased[currentItemIndex]);
        selectButton.gameObject.SetActive(HatsPurchased[currentItemIndex]);

        selectText.text = selectedIndex == currentItemIndex ? "Equipped" : "Select";

        buyButton.interactable = player.GetGold() >= hatPrice;
    }
    private void SetPlayerModelHat(Hat hat)
    {
        if (currentHatModel != null)
        {
            Destroy(currentHatModel);
        }
        currentHatModel = Instantiate(skinArraySO.GetHat(hat), headTransform);
        currentHatModel.layer = 9;
    }
    public override void BuyButton()
    {
        player.UpdateGold(-hatPrice);
        player.SetHat((Hat)currentItemIndex);
        selectedIndex = currentItemIndex;
        HatsPurchased[currentItemIndex] = true;
    }
    public override void SelectButton()
    {
        base.SelectButton();
        player.SetHat((Hat)currentItemIndex);
    }
    public override void SelectItem(int itemIndex)
    {
        base.SelectItem(itemIndex);
        
    }
}
