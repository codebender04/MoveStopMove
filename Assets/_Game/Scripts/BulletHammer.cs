using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHammer : BulletBase
{
    [SerializeField] private float rotatingSpeed;
    protected override void Update()
    {
        base.Update();
        bulletVisual.Rotate(0f, 0f, rotatingSpeed * Time.deltaTime);
    }
}
