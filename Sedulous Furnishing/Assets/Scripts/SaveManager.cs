using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/PlayerData";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/PlayerData";
        if (File.Exists(path))
        {
            Debug.Log("Save file founded in " + path);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data =  formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }


    public static void saveDate(timer t)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/timeData";
        FileStream stream = new FileStream(path, FileMode.Create);

        timerData data = new timerData(t);
        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static timerData loadDate()
    {
        string path = Application.persistentDataPath + "/timeData";
        if (File.Exists(path))
        {
            Debug.Log("Date file founded in " + path);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            timerData data = formatter.Deserialize(stream) as timerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Date file not found in " + path);
            return null;
        }
    }

    public static void saveGamehandler(GameHandler gh)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/ghData";
        FileStream stream = new FileStream(path, FileMode.Create);

        gamehandlerData data = new gamehandlerData(gh);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static gamehandlerData loadGamehandler()
    {
        string path = Application.persistentDataPath + "/ghData";
        if (File.Exists(path))
        {
            Debug.Log("Date file founded in " + path);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            gamehandlerData data = formatter.Deserialize(stream) as gamehandlerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Date file not found in " + path);
            return null;
        }
    }
}
