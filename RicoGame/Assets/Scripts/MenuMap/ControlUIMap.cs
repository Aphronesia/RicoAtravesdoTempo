using Game;
using UnityEngine;

namespace MenuMap {
    public class ControlUIMap : MonoBehaviour
    {
        [SerializeField]
        private LevelPopUp lvPop;
        private ControlScenes _controlScenes;
        private LevelManager _lvManager;

        private bool _pause;
        private void Start() {
            ControlScenes scenesController = FindObjectOfType<ControlScenes>();
            if (scenesController != null){
                _controlScenes = scenesController.GetComponent<ControlScenes>();
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
        public void PlayLevel(){
            int index = lvPop.SceneIndex();
            _controlScenes.ChangeScene(index);
        }
    }
}
