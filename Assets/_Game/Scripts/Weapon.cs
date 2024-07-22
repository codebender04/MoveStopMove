using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] public Transform weaponVisual;
    [SerializeField] protected WeaponSO weaponSO;

    private MeshRenderer meshRenderer;
    protected Character weaponOwner;
    private Vector3 startPosition;
    private void Awake()
    {
        meshRenderer = weaponVisual.GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        startPosition = transform.position;
        transform.localScale = weaponOwner.transform.localScale;
        rb.velocity = weaponSO.speed * transform.forward;
        SetSkin(0);
    }
    private void SetSkin(int skinIndex)
    {
        Material[] skinMaterials = new Material[meshRenderer.materials.Length];
        for (int i = 0; i < skinMaterials.Length; i++)
        {
            skinMaterials[i] = weaponSO.skins[skinIndex];
        }
        meshRenderer.materials = skinMaterials;
    }
    protected virtual void Update()
    {
        CheckRange();
    }
    public virtual void Initialize(Character weaponOwner)
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
