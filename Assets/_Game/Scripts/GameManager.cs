using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState { Playing , Victory , Lose }
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Player player;
    public GameState State = GameState.Playing;
    private List<ISaveable> saveables;
    private void Awake()
    {
        saveables = new List<ISaveable>() { player };
        SaveManager.LoadJsonData(saveables);
    }
    void Start()
    {
        UIManager.Instance.Open<CanvasMainMenu>();
        player.OnPlayerDeath += Player_OnPlayerDeath;
    }
    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        IndicatorManager.Instance.ClearIndicator();
        UIManager.Instance.CloseAll();
        if (State == GameState.Playing)
        {
            UIManager.Instance.Open<CanvasLose>();
            State = GameState.Lose; 
        }
    }
    private void OnApplicationQuit()
    {
        SaveManager.SaveJsonData(saveables);
    }
}
