using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureCapture : MonoBehaviour
{
    [SerializeField] private RenderTexture captureTexture;
    public void ExportPhoto()
    {
        byte[] bytes = ToTexture2D(captureTexture).EncodeToPNG();
        var dirPath = Application.persistentDataPath + "/ExportPhoto";
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        System.IO.File.WriteAllBytes(dirPath + "/Photo_" + Random.Range(0, 100000) + ".png", bytes);
        Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + dirPath); 
    }
    private Texture2D ToTexture2D(RenderTexture captureTexture)
    {
        Texture2D texture = new Texture2D(400, 300, TextureFormat.RGB24, false);
        RenderTexture.active = captureTexture;
        texture.ReadPixels(new Rect(0, 0, captureTexture.width, captureTexture.height), 0, 0);
        texture.Apply();
        return texture;
    }
}
