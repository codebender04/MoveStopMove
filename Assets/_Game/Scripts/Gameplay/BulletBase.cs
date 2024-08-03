using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] protected WeaponSO weaponSO;
    [SerializeField] protected Transform bulletVisual;

    private Vector3 startPosition;
    protected Character weaponOwner;
    private void Start()
    {
        startPosition = transform.position;
        rb.velocity = weaponSO.speed * transform.forward;
    }
    public void SetSkin(Material[] skinMaterials)
    {
        meshRenderer.materials = skinMaterials;
    }
    public void SetColor(int colorablePartIndex, Material color)
    {
        meshRenderer.materials[colorablePartIndex] = color;
    }
    public virtual void Initialize(Character weaponOwner)
    {
        this.weaponOwner = weaponOwner;
        transform.localScale = weaponOwner.transform.localScale;
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
    public Character GetWeaponOwner()
    {
        return weaponOwner;
    }
}
