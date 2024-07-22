using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BotManager : Singleton<BotManager>
{
    [SerializeField] private Bot botPrefab;
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private int concurrentBotNumber;
    [SerializeField] private int totalBotNumber;
    private List<Bot> botList = new List<Bot>();
    private IndicatorManager indicatorManager;
    private Bounds bounds;
    private IndicatorManager IndicatorManager
    {
        get
        {
            if (indicatorManager == null)
            {
                indicatorManager = FindObjectOfType<IndicatorManager>();
            }
            return indicatorManager;
        }
    }
    public List<Bot> BotList {  get { return botList; } }
    private void Awake()
    {
        bounds = navMeshSurface.navMeshData.sourceBounds;
    }
    private void Start()
    {
        for (int i = 0; i < concurrentBotNumber; i++)
        {
            SpawnBot();
        }
    }
    private void Update()
    {
        if (botList.Count < concurrentBotNumber)
        {
            for (int i = botList.Count; i < concurrentBotNumber; i++)
            {
                if (totalBotNumber <= 0) break;
                SpawnBot();
            }
        }
    }
    private void SpawnBot()
    {
        if (totalBotNumber <= 0) return;
        Bot bot = Instantiate(botPrefab, GetRandomPosition(), Quaternion.identity);
        botList.Add(bot);
        IndicatorManager.AddIndicator();
        totalBotNumber--;
    }
    public void RemoveBot(Bot bot)
    {
        botList.Remove(bot);
        if (IndicatorManager != null) IndicatorManager.RemoveIndicator();
    }
    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = Vector3.zero;
        int edge = Random.Range(0, 4);
        switch (edge)
        {
            //Top edge
            case 0: 
                randomPosition = new Vector3(Random.Range(bounds.min.x, bounds.max.x), 0, bounds.max.z); 
                break;
            //Bottom edge
            case 1:
                randomPosition = new Vector3(Random.Range(bounds.min.x, bounds.max.x), 0, bounds.min.z);
                break;
            //Left edge
            case 2:
                randomPosition = new Vector3(bounds.min.x, 0, Random.Range(bounds.min.z, bounds.max.z)); 
                break;
            //Right edge
            case 3:
                randomPosition = new Vector3(bounds.max.x, 0, Random.Range(bounds.min.z, bounds.max.z));
                break;
        }
        return randomPosition;
    }
}
