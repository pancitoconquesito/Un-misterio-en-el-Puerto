using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SAVE_LOAD
{
    public static void SAVE_DATA_GAME(DATA_GAME dataGame)
    {
        string dataPath = Application.persistentDataPath + "/game.save";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, dataGame);
        fileStream.Close();
    }
    public static DATA_GAME LOAD_DATA_GAME()
    {
        string dataPath = Application.persistentDataPath + "/game.save";
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            DATA_GAME data_game = (DATA_GAME)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return data_game;
        }
        else
        {
            Debug.LogError("LA DATA NO PUDO CARGARSE!!!");
            return null;
        }
    }
}
