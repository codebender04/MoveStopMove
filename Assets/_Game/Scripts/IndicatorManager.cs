using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : Singleton<IndicatorManager>
{
    [SerializeField] private GameObject indicator;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float edgeMargin;
    private Camera mainCamera;
    private List<GameObject> indicatorList = new List<GameObject>();
    private void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        if (GameManager.Instance.State != GameState.Playing)
        {
            return;
        }
        //Debug.Log(indicatorList.Count + " - " + BotManager.Instance.BotList.Count);
        while (indicatorList.Count < BotManager.Instance.BotList.Count)
        {
            AddIndicator();
        }
        for (int i = 0; i < BotManager.Instance.BotList.Count; i++)
        {
            UpdateIndicator(indicatorList[i], BotManager.Instance.BotList[i]);
        }
    }
    public void ClearIndicator()
    {
        while (indicatorList.Count > 0)
        {
            RemoveIndicator();
        }
    }
    public void AddIndicator()
    {
        if (indicatorList.Count >= BotManager.Instance.BotList.Count) return;
        GameObject indicator = Instantiate(this.indicator, transform);
        indicatorList.Add(indicator);
    }
    public void RemoveIndicator()
    {
        Destroy(indicatorList[^1]);
        indicatorList.Remove(indicatorList[^1]);
    }
    private void UpdateIndicator(GameObject indicator, Bot bot)
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(bot.transform.position);

        bool isOffScreen = screenPosition.x < 0 || screenPosition.x > Screen.width || 
                           screenPosition.y < 0 || screenPosition.y > Screen.height;

        indicator.SetActive(isOffScreen);

        if (isOffScreen)
        {
            screenPosition.x = Mathf.Clamp(screenPosition.x, edgeMargin, Screen.width - edgeMargin);
            screenPosition.y = Mathf.Clamp(screenPosition.y, edgeMargin, Screen.height - edgeMargin);

            // Convert screen position to canvas position
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPosition, canvas.worldCamera, out Vector2 canvasPosition);
            indicator.GetComponent<RectTransform>().localPosition = canvasPosition;

            // Adjust the rotation/direction of the indicator to point from the center of the screen to the bot position
            Vector3 direction = new Vector2(canvasPosition.x, canvasPosition.y);
            indicator.transform.up = direction;
        }
    }
}
