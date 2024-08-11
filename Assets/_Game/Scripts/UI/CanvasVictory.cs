using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    [SerializeField] private TextMeshProUGUI goldText;
    private void OnEnable()
    {
        goldText.text = GameManager.Instance.PlayerPoint.ToString();
    }
    public void NextLevelButton()
    {
        UIManager.Instance.Close<CanvasVictory>(0);
        LevelManager.Instance.LoadNextLevel();
        UIManager.Instance.Open<CanvasGameplay>();
    }
    public void HomeButton()
    {
        LevelManager.Instance.Destroylevel();
        UIManager.Instance.CloseAll();
        UIManager.Instance.Open<CanvasMainMenu>();
    }
}
