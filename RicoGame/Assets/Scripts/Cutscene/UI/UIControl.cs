using System;
using System.Collections;
using Game;
using Unity.VisualScripting;
using UnityEngine;

namespace Cutscene.UI{
    public class UIControl : MonoBehaviour {
        
        [SerializeField] private float durationFade;
        [SerializeField] private CanvasGroup UITransition; 
        
        [SerializeField]
        private Cutscene.CameraPivot cameraPivot;
        private ControlScenes _controlScenes;
        
        private void Start(){
            TakeComponents();
            UITransition.alpha = 1f;
            UITransition.interactable = false;
            UITransition.blocksRaycasts = false;
            StartCoroutine(FadeIn());
            
        }
        private IEnumerator FadeIn() {
            float startAlpha = UITransition.alpha;
            float time = 0f;
            while (time < durationFade) {
                time += Time.deltaTime;
                UITransition.alpha = Mathf.Lerp(startAlpha, 0f, time / durationFade);
                yield return null;
            }
            UITransition.alpha = 0f;
            UITransition.interactable = true;
            UITransition.blocksRaycasts = true;
            cameraPivot.GameStart(0);
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
