using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasLose : UICanvas
{
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI goldText;
    private Player player;
    private void OnEnable()
    {
        rankText.text = "Rank: #" + BotManager.Instance.GetAliveBot().ToString();
        goldText.text = GameManager.Instance.PlayerPoint.ToString(); 
    }
    public void RetryButton()
    {
        LevelManager.Instance.LoadCurrentLevel();
        Close(0);
        UIManager.Instance.Open<CanvasGameplay>();
    }
    public void HomeButton()
    {
        LevelManager.Instance.Destroylevel();
        UIManager.Instance.CloseAll();
        UIManager.Instance.Open<CanvasMainMenu>();
    }
}
