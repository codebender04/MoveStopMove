using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    [SerializeField] private float rotatingSpeed;
    protected override void Update()
    {
        base.Update();
        weaponVisual.Rotate(0f, 0f, rotatingSpeed * Time.deltaTime);
    }
}
