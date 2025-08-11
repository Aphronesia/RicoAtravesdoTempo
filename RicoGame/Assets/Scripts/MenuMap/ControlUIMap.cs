using Game;
using UnityEngine;

namespace MenuMap {
    public class ControlUIMap : MonoBehaviour
    {
        [SerializeField]
        private LevelPopUp lvPop;
        private ControlScenes _controlScenes;
        private SaveLoadSystem _saveLoadSystem;
        private LevelManager _lvManager;

        private bool _pause;
        
        
        private void Start() {
            TakeComponents();
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        public void PlayLevel(){
            int index = lvPop.SceneIndex();
            _controlScenes.ChangeScene(index);
        }

        public void PlayLevelWithCutscene(){
            if (lvPop.Cutscene()){
                _controlScenes.ProxLevel = lvPop.SceneIndex();
                _controlScenes.indexCutscene = lvPop.ActualCutsceneIndex();
                _controlScenes.ChangeScene(1);
            }
            else{
                _controlScenes.ChangeScene(lvPop.SceneIndex());
            }
            
        }
        
        public void SaveData() {
            _saveLoadSystem.SaveGameData();
        }

        public void LoadData() {
            _saveLoadSystem.LoadGameData();
        }
        
        
        private void TakeComponents() {
            ControlScenes scenesController = FindObjectOfType<ControlScenes>();
            if (scenesController != null){
                _controlScenes = scenesController.GetComponent<ControlScenes>();
            }
            SaveLoadSystem saveLoadSystem = FindObjectOfType<SaveLoadSystem>();
            if (saveLoadSystem != null) {
                _saveLoadSystem = saveLoadSystem.GetComponent<SaveLoadSystem>();
            }
            GameObject levelController = GameObject.Find("LevelController");
            if (levelController != null){
                _lvManager = levelController.GetComponent<LevelManager>();
            }
            GameObject levelPop = GameObject.Find("Canvas/LevelMenu");
            if(levelPop !=null){
                lvPop = levelPop.GetComponent<LevelPopUp>();
            }
        }
    }
}
