using System;
using System.IO;
using UnityEngine;

namespace Game
{
    public class SaveLoadSystem : MonoBehaviour{
        
        private ControlScenes _controlScenes;
        private Settings _settings;
        public SettingsData settingsData = new SettingsData();
        public GameData gameData = new GameData();
        public GameData runtimeGameData = new GameData();
        
        // Caminho dos Saves
        private string _pathSettings;
        private string _pathGame;

        public static event Action OnLoadGame;

        private void Awake(){
            if (FindObjectsOfType<SaveLoadSystem>().Length > 1){
                Destroy(gameObject);
            }
            else{
                DontDestroyOnLoad(gameObject);
            }

            CreateFolders();
            _pathSettings = Application.persistentDataPath + "/Saves/SettingsData.json";
            _pathGame = Application.persistentDataPath + "/Saves/GameData.json";

            settingsData = new SettingsData();
            gameData = new GameData();
        }

        private void Start(){
            TakeComponents();
            
            LoadSettingsData();
            LoadGameData();
        }
        
        
        private static void CreateFolders(){
            string basePath = Application.persistentDataPath;
            string folderName = "Saves";
            string fullPath = Path.Combine(basePath, folderName);

            try{
                if (!Directory.Exists(fullPath)){
                    Directory.CreateDirectory(fullPath);
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
            try{
                gameData = runtimeGameData;
                string json = JsonUtility.ToJson(gameData, true);
                File.WriteAllText(_pathGame, json);
                Debug.Log($"Salvo em = {_pathGame}");
            }
            catch (Exception ex){
                Debug.LogError(ex.ToString());
            }
        }

        public void ContinueGame(){
            runtimeGameData = gameData;
            
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

        public void ClearData(){
            // Não tá funcionando kk.
            string dataPath = Application.persistentDataPath;
            
            
            DirectoryInfo dir = new DirectoryInfo(dataPath);
            // Deleta todos os arquivos
            foreach (FileInfo file in dir.GetFiles()){
                try{
                    file.Delete();
                }
                catch (IOException ex){
                    Debug.LogError($"Erro ao deletar arquivo {file.FullName}: {ex.Message}");
                }
            }
            
            Debug.Log("todos os arquivos deletados");
            _controlScenes.RestartGame();
        }
        private void TakeComponents(){
            Settings gameSettings = FindObjectOfType<Settings>();
            if (gameSettings != null){
                _settings = gameSettings.GetComponent<Settings>();
            }
            ControlScenes controlScenes = FindObjectOfType<ControlScenes>();
            if (controlScenes != null){
                _controlScenes = controlScenes.GetComponent<ControlScenes>();
            }
        }
    
    }
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
}

