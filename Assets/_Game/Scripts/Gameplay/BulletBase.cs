using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] protected WeaponSO weaponSO;
    [SerializeField] private MeshRenderer meshRenderer;

    private Vector3 startPosition;
    protected Character weaponOwner;
    private void Start()
    {
        startPosition = transform.position;
        transform.localScale = weaponOwner.transform.localScale;
        rb.velocity = weaponSO.speed * transform.forward;
    }
    public virtual void Initialize(Character weaponOwner)
    {
        this.weaponOwner = weaponOwner;
    }
    protected virtual void Update()
    {
        CheckRange();
    }
    private void CheckRange()
    {
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);
        if (distanceTravelled >= weaponSO.range)
        {
            OnWeaponMaxRangeReached();
        }
    }
    protected virtual void OnWeaponMaxRangeReached()
    {
        Destroy(gameObject);
    }
}
