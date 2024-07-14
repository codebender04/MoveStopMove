using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] protected Transform weaponVisual;
    [SerializeField] private WeaponSO weaponSO;
    private Character weaponOwner;
    private Vector3 startPosition;
    private void Start()
    {
        startPosition = transform.position;
        rb.velocity = weaponSO.speed * transform.forward;
    }
    protected virtual void Update()
    {
        CheckRange();
    }
    public void Initialize(Character weaponOwner)
    {
        this.weaponOwner = weaponOwner;
    }
    private void CheckRange()
    {
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);
        if (distanceTravelled >= weaponSO.range)
        {
            OnWeaponMaxRangeReached();
        }
    }
    public Character GetWeaponOwner()
    {
        return weaponOwner;
    }
    protected virtual void OnWeaponMaxRangeReached()
    {
        Destroy(gameObject);
    }
}
