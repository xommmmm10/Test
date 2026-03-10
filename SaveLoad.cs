using UnityEngine;
using System.IO;

public static class SaveLoad
{
    public static void Save(string filename, object obj)
    {
        string json = JsonUtility.ToJson(obj, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, filename), json);
    }

    public static T Load<T>(string filename)
    {
        string path = Path.Combine(Application.persistentDataPath, filename);
        if (!File.Exists(path)) return default;
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(json);
    }
}
