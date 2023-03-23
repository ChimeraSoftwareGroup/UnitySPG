using System.Collections.Generic;
using UnityEngine;

public class JsonHelper
{
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}

[System.Serializable]
public class User
{
    public int id { get; set; }
    public string name { get; set; }
    public string picture { get; set; }
}

[System.Serializable]
public class Room
{
    public int id { get; set; }
    public string name { get; set; }
    public string password { get; set; }
}