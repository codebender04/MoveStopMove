using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    [SerializeField] protected SkinArraySO skinArraySO;
    [SerializeField] protected Button buyButton;
    [SerializeField] protected Button selectButton;
    [SerializeField] protected TextMeshProUGUI selectText;
    [System.Serializable] protected struct ButtonObject
    {
        public Outline outline;
        public GameObject lockImage;
    }
    [SerializeField] protected ButtonObject[] buttonObjectArray;
    protected Player player;
    protected int selectedIndex;
    protected int currentItemIndex;
    private void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }
    private void OnEnable()
    {
        UpdateTabUI();
    }
    public virtual void BuyButton()
    {

    }
    public virtual void SelectButton()
    {
        selectedIndex = currentItemIndex;
    }
    public virtual void UpdateTabUI()
    {
        
    }
    public virtual void SelectItem(int itemIndex)
    {
        currentItemIndex = itemIndex;
        for (int i = 0; i < buttonObjectArray.Length; i++)
        {
            buttonObjectArray[i].outline.enabled = i == itemIndex;
        }
        UpdateTabUI();
    }
    public int GetCurrentItemIndex()
    {
        return currentItemIndex;
    }
}
