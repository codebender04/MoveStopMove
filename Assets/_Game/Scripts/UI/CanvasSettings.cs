using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSettings : UICanvas
{
    public void HomeButton()
    {
        LevelManager.Instance.Destroylevel();
        UIManager.Instance.CloseAll();
        UIManager.Instance.Open<CanvasMainMenu>();
    }
}
