using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{

    public static void SavePlayer()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/PlayerData";
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData();
            formatter.Serialize(stream, data);
            stream.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
    }
    public static void SavePlayer(Player player,CslHandler cslHandler)
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/PlayerData";
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData(player,cslHandler);
            formatter.Serialize(stream, data);
            stream.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
    }

    public static PlayerData LoadPlayer()
    {

        string path = Application.persistentDataPath + "/PlayerData";
        try
        {
            if (File.Exists(path))
            {
                // Debug.Log("Player Save file founded in " + path);
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();

                return data;
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                // string path = Application.persistentDataPath + "/PlayerData";
                FileStream stream = new FileStream(path, FileMode.Create);

                PlayerData data = new PlayerData();
                formatter.Serialize(stream, data);
                stream.Close();

                return data;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
        
    }

    public static void saveDate()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/timeData";
            FileStream stream = new FileStream(path, FileMode.Create);

            timerData data = new timerData();
            formatter.Serialize(stream, data);
            stream.Close();
        }
        catch(Exception e){
            Debug.Log(e);
        }
        
    }

    public static void saveDate(timer t)
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/timeData";
            FileStream stream = new FileStream(path, FileMode.Create);

            timerData data = new timerData(t);
            formatter.Serialize(stream, data);
            stream.Close();
        }
        catch(Exception e){
            Debug.Log(e);
        }
        
    }

    public static timerData loadDate()
    {
        string path = Application.persistentDataPath + "/timeData";
        if (File.Exists(path))
        {
            // Debug.Log("Timer Date file founded in " + path);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            timerData data = formatter.Deserialize(stream) as timerData;
            stream.Close();

            return data;
        }
        else
        {
            // Debug.Log("Timer Date file not found in " + path);
            
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            timerData data = new timerData();
            formatter.Serialize(stream, data);
            stream.Close();

            return data;
        }
    }

    public static void saveInventory(FurnitureData[] furnitures){
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/InventoryData";
            FileStream stream = new FileStream(path, FileMode.Create);

            InventoryData data = new InventoryData(furnitures);
            formatter.Serialize(stream, data);
            stream.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public static InventoryData loadInventory(){
        string path = Application.persistentDataPath + "/InventoryData";
        try
        {
            if (File.Exists(path))
            {
                // Debug.Log("Player Save file founded in " + path);
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                InventoryData data = formatter.Deserialize(stream) as InventoryData;
                stream.Close();

                return data;
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }



}


/*
 * Inventroy ( cell[] )      array level 1
 * - Cell ( index position (int) )  array level 2
 * -- Furniture ( parts[] ) 
 * --- parts ( position, name, material )
 * ---- position ( float[3] )
 * ---- name ( String )
 * ---- material ( String )
 * 
 */
