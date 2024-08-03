using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasLose : UICanvas
{
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI goldText;
    private void OnEnable()
    {
        rankText.text = "#" + BotManager.Instance.GetAliveBot().ToString();
    }
    public void RetryButton()
    {
        LevelManager.Instance.LoadLevel();
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
