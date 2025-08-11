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
        
        public bool hasSettingsData, hasGameData;
        
        // Caminho dos Saves
        private string _pathSettings;
        private string _pathGame;

        [SerializeField] private bool pathInAssets;
        public static event Action OnLoadGame;

        private void Awake(){
            if (FindObjectsOfType<SaveLoadSystem>().Length > 1){
                Destroy(gameObject);
            }
            else{
                DontDestroyOnLoad(gameObject);
            }

            // falso para salvar na appdata
            pathInAssets = false;
            if (pathInAssets) {
                _pathSettings = Path.Combine(Application.dataPath, "/Saves/SettingsData.json");
                _pathGame = Path.Combine(Application.dataPath, "/Saves/GameData.json");
                CreateFolders(true);
                //Debug.Log("Vai ser salvo nos assets");
            }
            else {
                _pathSettings = Application.persistentDataPath + "/Saves/SettingsData.json";
                _pathGame = Application.persistentDataPath + "/Saves/GameData.json";
                CreateFolders(false);
                // Debug.Log("Vai ser salvo na AppData");
            }
            

            settingsData = new SettingsData();
            gameData = new GameData();
            hasSettingsData = false;
            hasGameData = false;
        }

        private void Start(){
            TakeComponents();
            
            LoadSettingsData();
            LoadGameData();
        }
        
        private static void CreateFolders(bool localSave) {
            string basePath;
            basePath = localSave ? Application.dataPath : Application.persistentDataPath;
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
            try {
                File.WriteAllText(_pathSettings, json);
            }
            catch (Exception ex) {
                Debug.LogError($"[Erro] {ex.Message}");
                throw;
            }
            
            //Debug.Log("salvo em = " + _pathSettings);
            hasSettingsData = true;
        }

        public void LoadSettingsData(){
            try{
                if (File.Exists(_pathSettings)){
                    string json = File.ReadAllText(_pathSettings);

                    JsonUtility.FromJsonOverwrite(json, settingsData);
                    hasSettingsData = true;
                    _settings.LoadSettings();
                    //OnLoadSettings?.Invoke();
                }
                else{
                    hasSettingsData = false;
                    settingsData.volumeMaster = 0.5f;
                    settingsData.volumeMusic = 0.5f;
                    settingsData.effects = true;
                    SaveSettingsData();
                    //Debug.Log("primeiro save settings");
                }
            }
            catch (Exception ex){
                SaveSettingsData();
                //Debug.LogWarning(ex.Message);
            }
        }

        public void SaveGameData(){
            try{
                gameData.menuMapRico = runtimeGameData.menuMapRico;
                gameData.levelCompleted = runtimeGameData.levelCompleted;
                gameData.recordPoinsTrem =  runtimeGameData.recordPoinsTrem;
                string json = JsonUtility.ToJson(gameData, true);
                File.WriteAllText(_pathGame, json);
                Debug.Log($"Salvo em = {_pathGame}");
                hasGameData = true;
            }
            catch (Exception ex){
                Debug.LogError(ex.ToString());
            }
        }

        public void ContinueGame(){
            //Debug.Log("continuou");
            runtimeGameData.menuMapRico = gameData.menuMapRico;
            runtimeGameData.levelCompleted = gameData.levelCompleted;
            runtimeGameData.recordPoinsTrem = gameData.recordPoinsTrem;

        }
        public void ClearRuntimeData(){
            GameData reset =  new GameData();
            runtimeGameData = reset;
            Debug.Log("runtimeData Limpo");
        }
        public void LoadGameData(){
            try{
                if (File.Exists(_pathGame)){
                    string json = File.ReadAllText(_pathGame);
                    JsonUtility.FromJsonOverwrite(json, gameData);

                    hasGameData = true;
                    OnLoadGame?.Invoke();
                }
                else{
                    hasGameData = false;
                    Debug.Log("Arquivo não existe");
                }
            }
            catch (Exception ex){
                Debug.LogError(ex.Message);
            }
        }

        public void ClearData(){
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
            
            //Debug.Log("todos os arquivos deletados");
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
        public int recordPoinsTrem;
    }
}

