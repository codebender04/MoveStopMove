using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.Open<CanvasMainMenu>();
    }

}
