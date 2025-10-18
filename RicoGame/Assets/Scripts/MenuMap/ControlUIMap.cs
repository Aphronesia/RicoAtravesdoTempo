using System.Collections;
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
        [SerializeField]
        private CanvasGroup _panelTransition;
        [SerializeField] private float transitionDuration;
        private ControlSounds _controlSounds;
        
        private void Start() {
            TakeComponents();
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            
            // SOMMMMM
             _controlSounds.PlayMusic("Menu");
        }
        public void PlayLevel(){
            int index = lvPop.SceneIndex();
            _controlScenes.ChangeScene(index);
        }

        public void PlayLevelWithCutscene(){
            _controlSounds.PlaySfx("Button");
            StartCoroutine(Transition());
            
        }

        private IEnumerator Transition()
        {
            _controlSounds.PlaySfx("Select");
            float startAlpha = _panelTransition.alpha;
            float time = 0f;
            while (time < transitionDuration){
                time += Time.deltaTime;
                _panelTransition.alpha = Mathf.Lerp(startAlpha, 1f, time / transitionDuration);
                yield return null;
            }
            _panelTransition.alpha = 1f;
            _panelTransition.interactable = true;
            _panelTransition.blocksRaycasts = true;
            ContinueTranstion();
        }

        private void ContinueTranstion()
        {
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
            var soundManager = FindAnyObjectByType<ControlSounds>();
            if (soundManager is not null)
            {
                _controlSounds = soundManager.GetComponent<ControlSounds>();
            }
        }
    }
}
