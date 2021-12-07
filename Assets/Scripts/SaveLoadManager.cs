using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private string dataPath;
    GameManager gMan;

    void Start()
    {
        dataPath = Application.persistentDataPath + "/save";
        gMan = GameObject.Find("roamingGameManager").GetComponent<GameManager>();
    }

    public void SaveGame(int slot)
    {
        //pVal = GameObject.Find("roamingGameManager").GetComponent<playerValues>();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dataPath + "/saveData" + slot + ".dat");
        PlayerData data = new PlayerData();
        data.Money = gMan.Money;
        data.HubLevel = gMan.HubLevel;
        data.StoryLevel = gMan.StoryLevel;

        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadGame(int slot)
    {
        //pVal = GameObject.Find("roamingGameManager").GetComponent<playerValues>();
        if (File.Exists(dataPath + "/saveData" + slot + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataPath + "/saveData" + slot + ".dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            gMan.Money = data.Money;
            gMan.HubLevel = data.HubLevel;
            gMan.StoryLevel = data.StoryLevel;

            Debug.Log("Load Complete!");
        }
        else
        {
            Debug.Log("Nothing to Load");
        }
    }
}

[Serializable]

class PlayerData
{
    public int slot;
    public DateTime DateTimeRecord;
    public float TimePlayed;
    public float DistanceRan;
    public float TimeRunning;

    public int Money;
    public int StoryLevel;
    public int HubLevel;
}
