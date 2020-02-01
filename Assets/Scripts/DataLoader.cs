using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class DataLoader : MonoBehaviour
{
    public static DataLoader instance;
    public Player currentPlayer;
    public string fileName;
    private StreamReader sr;
    private StreamWriter sw;
    private string jsonString;

    public string enemyFileName;
    public List<Enemy> enemyList;
    public Enemy[] enemyArray = new Enemy[3];

    Scene scene;

    void Awake()
    {
        instance = this;

        scene = SceneManager.GetActiveScene();
        // Debug.Log("Scene name: " + scene.name + "\tScene Index: " + scene.buildIndex);

        //TODO: Serialize List - https://medium.com/@defuncart/json-serialization-in-unity-9420abbce30b
        // enemyList = new List<Enemy>();
        // foreach (Enemy e in enemyArray)
        // {
        //     e.barrelTime = Random.Range(1.0f, 3.0f);
        //     e.throwDirection = new Vector2(Random.Range(-150f, 150f), Random.Range(-10f, 0f));
        //     enemyList.Add(e);
        // }

        // Debug.Log(JsonUtility.ToJson(enemyList, true));
        // sw = new StreamWriter(Application.persistentDataPath + "/" + enemyFileName, false);
        // sw.Write(JsonUtility.ToJson(enemyList, true));
        // sw.Close();
        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            sr = new StreamReader(Application.persistentDataPath + "/" + fileName);
            jsonString = sr.ReadToEnd();
            sr.Close();
            currentPlayer = JsonUtility.FromJson<Player>(jsonString);
            Debug.Log("Load Success");
        }
        else
        {
            currentPlayer = new Player();
            currentPlayer.items = 0;
            currentPlayer.lives = 3;
            currentPlayer.lastLevel = 1;
            Debug.Log("Player Set");
        }
    }

    public void WriteData()
    {
        sw = new StreamWriter(Application.persistentDataPath + "/" + fileName, false);
        sw.Write(JsonUtility.ToJson(currentPlayer, true));
        sw.Close();
        Debug.Log("Save Success");
    }
}
