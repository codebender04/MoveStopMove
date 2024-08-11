using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState { Playing , Victory , Lose }
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Player player;
    public GameState State = GameState.Playing;
    public int PlayerGold => player.GetGold();
    public int PlayerPoint => player.GetPoint();
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
        if (State == GameState.Playing)
        {
            State = GameState.Lose;
            UIManager.Instance.CloseAll();
            UIManager.Instance.Open<CanvasLose>();
        }
    }
    public void OnPlayerVictory()
    {
        State = GameState.Victory;
        player.OnVictory();
        UIManager.Instance.CloseAll();
        UIManager.Instance.Open<CanvasVictory>();
        
    }
    private void OnApplicationQuit()
    {
        SaveManager.SaveJsonData(saveables);
    }
}
