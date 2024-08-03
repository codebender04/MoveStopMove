using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGameplay : UICanvas
{
    [SerializeField] private TextMeshProUGUI aliveText;
    public void SettingsButton()
    {
        UIManager.Instance.Open<CanvasSettings>();
    }
    private void Update()
    {
        if (BotManager.Instance == null) return;
        aliveText.text = "Alive: " + BotManager.Instance.GetAliveBot().ToString();
    }
}
