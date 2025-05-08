using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game{
    public class Settings : MonoBehaviour
    {
        public float volumeMusic;
        public float voumeMaster;
        public bool effects;
        
        private void Awake() {
            if (FindObjectsOfType<Settings>().Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

}
