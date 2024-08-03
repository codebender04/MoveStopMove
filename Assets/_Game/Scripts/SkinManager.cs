using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkinManager
{
    public static Material[] SetSkin(Renderer baseModel, Material skinMaterial)
    {
        Material[] skinMaterials = new Material[baseModel.materials.Length];
        for (int i = 0; i < skinMaterials.Length; i++)
        {
            skinMaterials[i] = skinMaterial;
        }
        baseModel.materials = skinMaterials;
        return skinMaterials;
    }
}
