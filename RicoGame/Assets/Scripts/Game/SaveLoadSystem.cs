using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    [Serializable]
    public class GameData{
        //Settings
        public float volumeMusic;
        public float volumeMaster;
        public bool effects; 
        
        //Save
        public int levelCompleted;
    }

    public GameData runtimeData = new GameData();
    private string path;
    private void Awake() {
        path = Application.persistentDataPath + "/Gamedata.json";
    }

    public void SaveData(){

        GetData();
        string json = JsonUtility.ToJson(runtimeData, true);

        File.WriteAllText(path, json);

        Debug.Log("salvo em = " + path);

    }
    public void GetData(){
        
    }
    public void LoadData(){
        if(File.Exists(path)){
            string json = File.ReadAllText(path);

            JsonUtility.FromJsonOverwrite(json, runtimeData);
        } else {
            Debug.LogError("arquivo não existe");
        }
    }
}

