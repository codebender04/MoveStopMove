using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSkinShop : UICanvas
{
    [SerializeField] private Tab[] tabArray;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private SkinArraySO skinArraySO;
    private Player player;
    private int currentTabIndex = 0;
    private void OnEnable()
    {
        if (player == null) player = FindObjectOfType<Player>(); 

        goldText.text = player.GetGold().ToString();
        player.OnGoldChanged += Player_OnGoldChanged;
    }

    private void Player_OnGoldChanged(object sender, Player.OnGoldChangedEventArgs e)
    {
        goldText.text = e.gold.ToString();
    }

    public void SelectTab(int tabIndex)
    {
        for (int i = 0; i < tabArray.Length; i++)
        {
            tabArray[i].gameObject.SetActive(false);
        }
        tabArray[tabIndex].gameObject.SetActive(true);
        currentTabIndex = tabIndex;
    }
    private void UpdateSkinShopUI()
    {
        tabArray[currentTabIndex].UpdateTabUI();
    }
    public void BuyButton()
    {
        tabArray[currentTabIndex].BuyButton();

        UpdateSkinShopUI();
    }
    public void SelectButton()
    {
        tabArray[currentTabIndex].SelectButton();

        UpdateSkinShopUI();
    }
}
