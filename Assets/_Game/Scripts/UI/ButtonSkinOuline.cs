using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSkinOuline : MonoBehaviour
{
    [SerializeField] private Outline outline;
    [SerializeField] private int skinIndex;
    [SerializeField] private Tab tab;
    public void SwitchOutline()
    {
        outline.enabled = skinIndex == tab.GetCurrentItemIndex();
    }
}
