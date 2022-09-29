using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveManager<T> where T : class
{
    private static string StringPath()
    {
        return $"{Application.persistentDataPath}/Astrofire Arena";
    }

    public static void Save(T data, string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Create($"{StringPath()}/{name}.aa");
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static T Load(string name)
    {
        if (!File.Exists($"{StringPath()}/{name}"))
        {
            Debug.Log($"No {name} data exists!");
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream($"{StringPath()}/{name}", FileMode.Open);
        T data = formatter.Deserialize(stream) as T;
        Debug.Log($"Loaded {name} data successfully");
        return data;
    }
}
