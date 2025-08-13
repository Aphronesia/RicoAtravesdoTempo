using System;
using UnityEngine;

namespace Game{
    public class Settings : MonoBehaviour
    {
        private SaveLoadSystem _saveLoadSystem;
        public float volumeMusic;
        public float volumeMaster;
        public bool effects;
        
        public static event Action OnLoadSettings;
        
        
        // SOM FAZER EM UM SCRIPT OU OBJETO SEPARADO DEPOIS
        [SerializeField]
        private AudioSource audioSource;
        public AudioClip[] sounds;
        private int actualsound;
        
        public void LoadSettings() {
            if(_saveLoadSystem.settingsData != null) {
                volumeMusic = _saveLoadSystem.settingsData.volumeMusic;
                volumeMaster = _saveLoadSystem.settingsData.volumeMaster;
                effects = _saveLoadSystem.settingsData.effects;
                OnLoadSettings?.Invoke();
                
                // SOM
                ChangeVolume();
            }
        }
        private void Awake() {
            if (FindObjectsOfType<Settings>().Length > 1) {
                Destroy(gameObject);
            }
            else {
                DontDestroyOnLoad(gameObject);
            }
            GameObject saveLoadManager =  GameObject.Find("SaveLoadManager");
            if (saveLoadManager != null){
                _saveLoadSystem = saveLoadManager.GetComponent<SaveLoadSystem>();
            }

            actualsound = 90;
        }

        private void Start() {
            LoadSettings();
            
            //SOM   
            audioSource = GetComponent<AudioSource>();
        }

        public void SaveSettings(){
            _saveLoadSystem.settingsData.volumeMusic = volumeMusic;
            _saveLoadSystem.settingsData.volumeMaster = volumeMaster;
            _saveLoadSystem.settingsData.effects = effects;

            _saveLoadSystem.SaveSettingsData();
            
            // SOOOOMMM
            ChangeVolume();
        }

        public void SoundControllerMenu(int index) {
            if (index != actualsound) {
                audioSource.Stop();
                if (index >= 0 && index < sounds.Length)
                {
                    audioSource.clip = sounds[index]; //define o clip correspondente
                    audioSource.Play(); //toca o som
                    actualsound = index;
                }
                else
                {
                    Debug.LogWarning($"Indice fora dos limites do array de sons.");
                }
            }
        }
        public void ChangeVolume()
        {
            //garante q o valor esta entre 0.0 e 1.0 
            //se menor q 0.0 volume == 0.0 
            //se maior q 1.0 volume == 1.0 
            
            
                // volumeMusic = Mathf.Clamp(volumeMusic, 0.0f, 1.0f);
            //altera o volume
            
            audioSource.volume = volumeMusic;
        }
    }
}
