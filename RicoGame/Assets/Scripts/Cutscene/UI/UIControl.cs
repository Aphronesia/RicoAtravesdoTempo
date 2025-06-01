using System;
using Game;
using UnityEngine;

namespace Cutscene.UI{
    public class UIControl : MonoBehaviour
    {
        private ControlScenes _controlScenes;

        private void Start(){
            TakeComponents();
        }

        public void SkipCutscene(){
            _controlScenes.ChangeScene(2); // menu map
        }
        private void TakeComponents(){
            var controlScenes = FindObjectOfType<ControlScenes>();
            if (controlScenes != null){
                _controlScenes = controlScenes.GetComponent<ControlScenes>();
            }
        }
    }
}
