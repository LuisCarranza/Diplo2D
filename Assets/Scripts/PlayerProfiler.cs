using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerProfiler : MonoBehaviour
{
    public static PlayerProfiler instance;
    public int itemsCount;
    public int lives;

    private StreamReader sr;
    private StreamWriter sw;
    private string line;
    private string[] tmpData;
    private char[] delimiterChars = { '|', ',' };
    private int counterlines = 0;


    void Awake()
    {
        instance = this;
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
        {
            sr = new StreamReader(Application.persistentDataPath + "/PlayerData.dat");
            while ((line = sr.ReadLine()) != null)
            {
                if (counterlines == 0)
                {
                    tmpData = line.Split(delimiterChars);
                    itemsCount = int.Parse(tmpData[0]);
                    lives = int.Parse(tmpData[1]);
                    counterlines++;
                }
            }
            sr.Close();
        }
    }

    public void SaveData()
    {
        line = itemsCount + "|" + lives;
        sw = new StreamWriter(Application.persistentDataPath + "/PlayerData.dat", false);
        print(Application.persistentDataPath + "/PlayerData.dat");
        sw.WriteLine(line);
        sw.Close();
    }
}
