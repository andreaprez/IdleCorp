using System.IO;
using UnityEngine;

namespace IdleCorp.OOP.Services.UserData
{
    public class JsonDataHandler
    {
        public void SaveData<T>(T data)  
        {
            var fileName = data.GetType().Name;
            var path = Application.persistentDataPath + "/" + fileName;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);
        }

        public T ReadData<T>() 
        {
            var fileName = typeof(T).Name;
            var path = Application.persistentDataPath + "/" + fileName;

            if (!File.Exists(path)) 
                return default;

            var json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }
    }
}