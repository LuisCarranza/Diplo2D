using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataLoader : MonoBehaviour
{
    public static DataLoader instance;
    public Player currentPlayer;
    public string fileName;
    private StreamReader sr;
    private StreamWriter sw;
    private string jsonString;

    void Awake()
    {
        instance = this;
        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            sr = new StreamReader(Application.persistentDataPath + "/" + fileName);
            jsonString = sr.ReadToEnd();
            sr.Close();
            currentPlayer = JsonUtility.FromJson<Player>(jsonString);
        }
        else
        {
            currentPlayer = new Player();
            currentPlayer.items = 0;
            currentPlayer.lives = 3;
            currentPlayer.lastLevel = 0;
        }
    }

    public void WriteData()
    {
        sw = new StreamWriter(Application.persistentDataPath + "/" + fileName, false);
        sw.Write(JsonUtility.ToJson(currentPlayer));
        sw.Close();
    }
}
