using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<GameObject> levelArray;
    private GameObject currentLevel;
    private int levelIndex = 0;
    public void LoadLevel()
    {
        currentLevel = Instantiate(levelArray[levelIndex]);
    }
    private void Destroylevel()
    {
        Destroy(currentLevel);
    }
}
