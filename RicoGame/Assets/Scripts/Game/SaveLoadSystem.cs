using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SaveLoadSystem : MonoBehaviour
    {
        [Serializable]
        public class SettingsData{
            public float volumeMusic;
            public float volumeMaster;
            public bool effects;
        }
        [Serializable]
        public class GameData{
            public int levelCompleted;
            public int menuMapRico;

        }
        private Settings _settings;
        public SettingsData settingsData = new SettingsData();
        public GameData gameData = new GameData();
        private string _pathSettings;
        private string _pathGame;

        public static event Action OnLoadSettings;
        public static event Action OnLoadGame;
        private void Awake(){
            if (FindObjectsOfType<SaveLoadSystem>().Length > 1){
                Destroy(gameObject); 
            }
            else{
                DontDestroyOnLoad(gameObject);
            }
            CreateFolder();
            _pathSettings = Application.persistentDataPath + "/Saves/SettingsData.json";
            _pathGame = Application.persistentDataPath + "/Saves/GameData.json";

            settingsData = new SettingsData();
            gameData = new GameData();
        }

        private void Start(){
            Settings GameSettings = FindObjectOfType<Settings>();
            if (GameSettings != null){
                _settings = GameSettings.GetComponent<Settings>();
            }
            LoadSettingsData();
        }
        private static void CreateFolder(){
            string basePath = Application.persistentDataPath;
            string folderName = "Saves";
            string fullPath = Path.Combine(basePath, folderName);

            try{
                if (!Directory.Exists(fullPath)){
                    Directory.CreateDirectory(fullPath);
                    OnLoadGame?.Invoke();
                }
            }
            catch (Exception ex){
                Debug.LogError($"[Erro] {ex.Message}");
            }

        }
        public void SaveSettingsData(){
            string json = JsonUtility.ToJson(settingsData, true);
            File.WriteAllText(_pathSettings, json);
            Debug.Log("salvo em = " + _pathSettings);
        }
        public void LoadSettingsData(){
            try{
                if (File.Exists(_pathSettings)){
                    string json = File.ReadAllText(_pathSettings);

                    JsonUtility.FromJsonOverwrite(json, settingsData);
                    
                    _settings.LoadSettings();
                    //OnLoadSettings?.Invoke();
                }
                else{
                    SaveSettingsData();
                    Debug.Log("primeiro save settings");
                }
            }
            catch (Exception ex){
                Debug.LogWarning(ex.Message);
            }
        }
        public void SaveGameData(){
            try {
                string json = JsonUtility.ToJson(gameData, true);
                File.WriteAllText(_pathGame, json);
                Debug.Log($"Salvo em = {_pathGame}");
            }
            catch(Exception ex){
                Debug.LogError(ex.ToString());
            }
        }

        public void LoadGameData(){
            try{
                if (File.Exists(_pathGame)){
                    string json = File.ReadAllText(_pathGame);
                    JsonUtility.FromJsonOverwrite(json, gameData);

                    
                    OnLoadGame?.Invoke();
                }
                else{
                    Debug.LogError("Arquivo não existe");
                }
            }
            catch (Exception ex){
                Debug.LogError(ex.Message);
            }
        }
        
    }
}

