using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Weapon
{
    [SerializeField] private float rotatingSpeed;
    protected override void Update()
    {
        base.Update();
        weaponVisual.Rotate(0f, 0f, rotatingSpeed * Time.deltaTime);
    }
    protected override void OnWeaponMaxRangeReached()
    {
        StartCoroutine(nameof(ReturnToWeaponOwner));
    }
    private IEnumerator ReturnToWeaponOwner()
    {
        while (Vector3.Distance(transform.position, weaponOwner.transform.position + Vector3.up) > 0.1)
        {
            transform.position = Vector3.MoveTowards(transform.position, weaponOwner.transform.position + Vector3.up, weaponSO.speed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
