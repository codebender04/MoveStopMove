using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] private BulletBase bulletBasePrefab;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] protected WeaponSO weaponSO;
    protected Character weaponOwner;
    private void Start()
    {
        SetSkin(0);
    }
    public void OnFire(Vector3 pos, Quaternion rot)
    {
        Instantiate(bulletBasePrefab, pos, rot).Initialize(weaponOwner);
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
    public virtual void Initialize(Character weaponOwner)
    {
        this.weaponOwner = weaponOwner;
    }
}
