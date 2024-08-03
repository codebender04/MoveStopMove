using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSkinShop : UICanvas
{
    [SerializeField] private GameObject[] tabArray;
    [SerializeField] private Transform headTransform;
    [SerializeField] private Renderer pantsRenderer;
    [SerializeField] private SkinArraySO skinArraySO;
    private GameObject currentHat;
    private Player player;
    private void OnEnable()
    {
        if (player == null) { player = FindObjectOfType<Player>(); }
    }
    public void SelectTab(int tabIndex)
    {
        for (int i = 0; i < tabArray.Length; i++)
        {
            tabArray[i].SetActive(false);
        }
        tabArray[tabIndex].SetActive(true);
    }
    public void SelectPants(int pants)
    {
        player.SetPants((Pants)pants);
        SetPlayerModelPants((Pants)pants);
    }
    public void SelectHat(int hat)
    {
        player.SetHat((Hat)hat);
        SetPlayerModelHat((Hat)hat);
    }
    private void SetPlayerModelHat(Hat hat)
    {
        if (currentHat != null)
        {
            Destroy(currentHat);
        }
        currentHat = Instantiate(skinArraySO.GetHat(hat), headTransform);
        currentHat.layer = 9;
    }
    private void SetPlayerModelPants(Pants pants)
    {
        pantsRenderer.material = skinArraySO.GetPants(pants);
    }
}
