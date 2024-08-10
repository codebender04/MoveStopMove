using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<GameObject> levelArray;
    [SerializeField] private Player player;
    [SerializeField] private CameraFollow cameraFollow;
    private GameObject currentLevel;
    private int levelIndex = 0;
    public void LoadLevel(int levelIndex)
    {
        if (currentLevel != null)
        {
            Destroylevel();
        }
        currentLevel = Instantiate(levelArray[levelIndex]);
        player.Initialize();
        cameraFollow.ResetOffset();
        GameManager.Instance.State = GameState.Playing;
    }
    public void LoadCurrentLevel()
    {
        LoadLevel(levelIndex);
    }
    public void LoadNextLevel()
    {
        if (levelIndex > levelArray.Count)
        {
            UIManager.Instance.CloseAll();
            UIManager.Instance.Open<CanvasMainMenu>();
            return;
        }
        levelIndex++;
        LoadLevel(levelIndex);
    }
    public void Destroylevel()
    {
        player.Initialize();
        Destroy(currentLevel);
    }
}
