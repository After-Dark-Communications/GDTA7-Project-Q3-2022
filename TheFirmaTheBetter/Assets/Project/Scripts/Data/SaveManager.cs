using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[System.Serializable]
public class SaveManager
{
    private static string StringPath()
    {
        return Application.persistentDataPath;
    }

    /// <summary>
    /// Saves data of type T
    /// </summary>
    /// <param name="data">The data to save</param>
    /// <param name="name">The name the savefile will be called</param>
    public static void Save<T>(T data, string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream($"{StringPath()}/{name}.aa", FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// Loads the data
    /// </summary>
    /// <param name="name">The name under which the file was saved</param>
    /// <returns>Save data if present, null if none exist</returns>
    public static T Load<T>(string name)
    {
        if (!File.Exists($"{StringPath()}/{name}"))
        {
            Debug.Log($"No {name} data exists!");
            return default(T);
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream($"{StringPath()}/{name}", FileMode.Open);
        T data = (T)formatter.Deserialize(stream);
        Debug.Log($"Loaded {name} data successfully");
        stream.Close();
        return data;
    }
}
