using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabPants : Tab
{
    [SerializeField] private Renderer pantsRenderer;
    [SerializeField] private int pantsPrice;
    private bool[] PantsPurchased => player.PantsPurchased;
    public override void BuyButton()
    {
        player.UpdateGold(-pantsPrice);
        player.SetPants((Pants)currentItemIndex);
        PantsPurchased[currentItemIndex] = true;
        selectedIndex = currentItemIndex;
    }
    public override void SelectButton()
    {
        base.SelectButton();
        player.SetPants((Pants)currentItemIndex);
    }
    private void SetPlayerModelPants(Pants pants)
    {
        pantsRenderer.material = skinArraySO.GetPants(pants);
    }
    public override void UpdateTabUI()
    {
        SetPlayerModelPants((Pants)currentItemIndex);

        for (int i = 0; i < buttonObjectArray.Length; i++)
        {
            buttonObjectArray[i].lockImage.SetActive(!player.PantsPurchased[i]);
        }

        buyButton.gameObject.SetActive(!PantsPurchased[currentItemIndex]);
        selectButton.gameObject.SetActive(PantsPurchased[currentItemIndex]);

        selectText.text = selectedIndex == currentItemIndex ? "Equipped" : "Select";

        buyButton.interactable = player.GetGold() >= pantsPrice;
    }
}
