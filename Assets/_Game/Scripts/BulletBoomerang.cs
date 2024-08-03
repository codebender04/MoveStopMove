using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoomerang : BulletBase
{
    [SerializeField] private float rotatingSpeed;
    private Vector3 weaponOwnerPosition;
    protected override void Update()
    {
        base.Update();
        bulletVisual.Rotate(0f, 0f, rotatingSpeed * Time.deltaTime);
    }
    public override void Initialize(Character weaponOwner)
    {
        base.Initialize(weaponOwner);
        weaponOwnerPosition = weaponOwner.transform.position + Vector3.up;
    }
    protected override void OnWeaponMaxRangeReached()
    {
        StartCoroutine(nameof(ReturnToWeaponOwner));
    }
    private IEnumerator ReturnToWeaponOwner()
    {
        while (weaponOwner != null && Vector3.Distance(transform.position, weaponOwner.transform.position + Vector3.up) > 0.1)
        {
            weaponOwnerPosition = weaponOwner.transform.position + Vector3.up;
            transform.position = Vector3.MoveTowards(transform.position, weaponOwner.transform.position + Vector3.up, weaponSO.speed * Time.deltaTime);
            yield return null;
        }
        if (weaponOwner == null) //If the boomerang thrower dies while the boomerang is still flying, the boomerang returns to the last set position.
        {
            while (Vector3.Distance(transform.position, weaponOwnerPosition) > 0.1)
            {
                transform.position = Vector3.MoveTowards(transform.position, weaponOwnerPosition, weaponSO.speed * Time.deltaTime);
                yield return null;
            }
        }
        Destroy(gameObject);
    }
}
