using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public static class FileManager
{
    public static bool WriteToFile(string fileName, string fileContent)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            File.WriteAllText(fullPath, fileContent);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {e}");
            return false;
        }
    }
    public static bool LoadFromFile(string fileName, out string result)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            result = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {fullPath} with exception {e}");
            result = "";
            return false;
        }
    }
}
