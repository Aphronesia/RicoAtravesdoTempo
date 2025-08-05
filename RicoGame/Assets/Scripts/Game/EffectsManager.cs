using System;
using UnityEngine;

namespace Game
{
    public class EffectsManager : MonoBehaviour
    {
        private Settings _settings;
        [SerializeField]
        private MonoBehaviour[] _scrptsToDisable;
        private void Start()
        {
            TakeComponents();
        }

        public void ApplyChanges(bool effectsValue)
        {
            foreach (MonoBehaviour script in _scrptsToDisable)
            {
                script.enabled = effectsValue;
            }
        }
        private void TakeComponents()
        {
            Settings gameSettings = FindObjectOfType<Settings>();
            if (gameSettings != null){
                _settings = gameSettings.GetComponent<Settings>();
            }
        }
    }
}
