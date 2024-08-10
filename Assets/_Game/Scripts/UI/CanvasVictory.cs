using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasVictory : UICanvas
{
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
