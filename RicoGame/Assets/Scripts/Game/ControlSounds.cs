using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game 
{
    public class ControlSounds : MonoBehaviour
    {   
        [Header("Volumes")]
        [Range(0, 1)] public float volumeMusic;
        [Range(0, 1)] public float volumeSfx;
        
        [Header("Sounds")]
        [SerializeField]
        private List<Audio> musics = new List<Audio>();
        [SerializeField]
        private List<Audio> soundEffects = new List<Audio>();

        private Audio _actualMusic;
        private Audio _actualSfx;
        
        [Header("Audios Sources")]
        [SerializeField]
        private AudioSource musicSource;
        [SerializeField]
        private AudioSource sfxSource;
        
        // Componentes  
        private Settings _settings;

        public static event Action OnChangeVolumes;
        private void OnValidate()
        {
            musicSource.volume = volumeMusic;
            sfxSource.volume = volumeSfx;
        }

        private void Awake()
        {
            BecomeIndestructible();
        }
        private void BecomeIndestructible()
        {
            if (FindObjectOfType<ControlSounds>() == null) 
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        
        private void Start()
        {
            TakeComponents();
        }

        public void PlayMusic(string clipName)
        {
            Audio music = musics.Find(x => x.name == clipName);
            if (music == null)
            {
                Debug.Log($"Musica '{clipName}' não encontrado");
                return;
            }
            if (music == _actualMusic)
            {
                return;
            }
            if (music.clip == null)
            {
                Debug.LogError($"musica: {music.name} sem SoundClip");
                return;
            }
            musicSource.Stop();
            musicSource.clip = music.clip;
            musicSource.Play();
            _actualMusic =  music;
        }
        
        public void PlaySfx(string clipName)
        {
            Audio sfx = soundEffects.Find(x => x.name == clipName);
            if (sfx == null)
            {
                Debug.Log($"Efeito Sonoro '{clipName}' não encontrado");
                return;
            }
            if (sfx.clip == null)
            {
                Debug.LogError($"Efeito Sonoro: {sfx.name} sem SoundClip");
                return;
            }
            sfxSource.Stop();
            sfxSource.clip = sfx.clip;
            sfxSource.Play();
            _actualSfx =  sfx;
            Debug.Log($"tocou som {sfx.clip.name} com volume {sfxSource.volume}");
        }

        public void ChangeVolumes()
        {
            if (_settings == null)
            {
                _settings = FindObjectOfType<Settings>();
            }

            volumeMusic = _settings.volumeMusic;
            volumeSfx = _settings.volumeMaster;
            musicSource.volume = volumeMusic;
            sfxSource.volume = volumeSfx;

            OnChangeVolumes?.Invoke();
        }
        
        private void TakeComponents()
        {
            var settings = GameObject.Find("GameSettings");
            if (settings != null)
            {
                _settings = settings.GetComponent<Settings>();
            }
        }
    }
    [Serializable]
    public class Audio
    {
        public string name;
        public AudioClip clip;
        [Range(-1, 1)] public float volumeMax;
    }
}
