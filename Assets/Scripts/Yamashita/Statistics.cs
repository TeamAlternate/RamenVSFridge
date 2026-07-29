using UnityEngine;
using System.IO;
public class Statistics 
{
    private static Statistics current;

    public int ramenWinCount;
    public int fridgeWinCount;

    [RuntimeInitializeOnLoadMethod]
    private static void RuntimeInitialize()
    {
        string path = GetSaveFilePath();
        current = LoadFromFile(path);
    }

    private static string GetSaveFilePath()
    {
#if UNITY_EDITOR
        string path = "./Saves.json";
#else
        string path = Application.dataPath + System.IO.Path.DirectorySeparatorChar + "Saves.json";
#endif
        return path;
    }

    private static Statistics LoadFromFile(string path)
    {
        Statistics result = new Statistics();
        if (File.Exists(path))
        {
            string json;
            using (StreamReader sr = new StreamReader(path))
            {
                json = sr.ReadToEnd();
            }
            JsonUtility.FromJsonOverwrite(json, result);
        }
        else
        {
            result.ramenWinCount = 0;
            result.fridgeWinCount = 0;
        }
        return result;
    }

    public void Save()
    {
        string path = GetSaveFilePath();
        string json = JsonUtility.ToJson(current);
        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.WriteLine(json);
        }
    }
}
