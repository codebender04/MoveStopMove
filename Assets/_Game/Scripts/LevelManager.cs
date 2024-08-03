using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<GameObject> levelArray;
    [SerializeField] private Player player;
    private GameObject currentLevel;
    private int levelIndex = 0;
    public void LoadLevel()
    {
        if (currentLevel != null)
        {
            Destroylevel();
        }
        currentLevel = Instantiate(levelArray[levelIndex]);
        player.Initialize();
        GameManager.Instance.State = GameState.Playing;
    }
    public void Destroylevel()
    {
        Destroy(currentLevel);
    }
}
