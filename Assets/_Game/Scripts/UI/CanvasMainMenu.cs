using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] private TextMeshProUGUI goldText;
    private Player player;
    private void OnEnable()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        player.OnGoldChanged += Player_OnGoldChanged;    
    }
    private void OnDisable()
    {
        player.OnGoldChanged -= Player_OnGoldChanged;
    }
    private void Player_OnGoldChanged(object sender, Player.OnGoldChangedEventArgs e)
    {
        goldText.text = e.gold.ToString();
    }
    public void PlayButton()
    {
        Close(0);
        UIManager.Instance.Open<CanvasGameplay>();
        LevelManager.Instance.LoadLevel();
    }
    public void WeaponShopButton()
    {
        UIManager.Instance.Open<CanvasWeaponShop>();
    }
    public void SkinShopButton()
    {
        UIManager.Instance.Open<CanvasSkinShop>();
    }
}
