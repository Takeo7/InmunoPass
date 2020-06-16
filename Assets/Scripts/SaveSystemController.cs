using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystemController
{
    
    public static void SaveUser(UserInfo userInf)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/userData.pass";

        FileStream stream = new FileStream(path, FileMode.Create);

        UserData data = new UserData(userInf);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static UserData LoadUser()
    {
        string path = Application.persistentDataPath + "/userData.pass";
        Debug.Log("Path: " + path);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            UserData data = formatter.Deserialize(stream) as UserData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("No data on path " + path);
            return null;
        }
    }

    public static void DeleteInfo()
    {
        string path = Application.persistentDataPath + "/userData.pass";

        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            Debug.LogWarning("No data on path " + path);
            
        }
    }

}
