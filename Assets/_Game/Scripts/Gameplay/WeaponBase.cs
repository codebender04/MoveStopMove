using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] private BulletBase bulletBasePrefab;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] protected WeaponSO weaponSO;
    public int ColorableParts => meshRenderer.materials.Length;

    protected Character weaponOwner;
    public void OnFire(Transform fireTransform)
    {
        Instantiate(bulletBasePrefab, fireTransform.position, fireTransform.rotation).Initialize(weaponOwner);
    }
    public void SetSkin(int skinIndex)
    {
        bulletBasePrefab.SetSkin(SkinManager.SetSkin(meshRenderer, weaponSO.skins[skinIndex]));
    }
    public void SetColor(int colorablePartIndex, Material color)
    {
        meshRenderer.materials[colorablePartIndex] = color;
        bulletBasePrefab.SetColor(colorablePartIndex, color);
    }
    public virtual void Initialize(Character weaponOwner)
    {
        this.weaponOwner = weaponOwner;
    }
}
