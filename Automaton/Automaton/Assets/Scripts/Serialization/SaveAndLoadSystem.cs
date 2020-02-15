using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

//Would have handled how a save file is created and loaded based on serialized data from another class.
//Part of the saving and loading system that was never fully implemented and was removed from the final game.

public class SaveAndLoadSystem
{
    public static void Save(Player player)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.dat";
        FileStream fileStream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(player);
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static SaveData Load()
    {
        string path = Application.persistentDataPath + "/save.dat";

        if(File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            SaveData data = binaryFormatter.Deserialize(fileStream) as SaveData;
            fileStream.Close();

            return data;
        }

        else
        {
            Debug.Log("ERROR! Save not found in " + path);
            return null;
        }
    }
}
