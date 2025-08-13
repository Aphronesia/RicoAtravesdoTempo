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
        
        [Serializable]
        private class Audio
        {
            public string name;
            public AudioClip clip;
            [Range(-1, 1)] public float volumeSpecific;
        }
        [Header("Audios Sources")]
        [SerializeField]
        private AudioSource musicSource;
        [SerializeField]
        private AudioSource sfxSource;
        
        // Componentes  
        private Settings _settings;

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
            if (sfx == _actualSfx)
            {
                return;
            }
            if (sfx.clip == null)
            {
                Debug.LogError($"Efeito Sonoro: {sfx.name} sem SoundClip");
                return;
            }
            musicSource.Stop();
            musicSource.clip = sfx.clip;
            musicSource.Play();
            _actualMusic =  sfx;
        }

        public void ChangeVolumes()
        {
            volumeMusic = Mathf.Clamp01(_settings.volumeMusic);
            volumeSfx = Mathf.Clamp01(_settings.volumeMaster);
            musicSource.volume = volumeMusic;
            sfxSource.volume = volumeSfx;
        }
        
        private void TakeComponents()
        {
            var settings = FindObjectOfType<Settings>();
            if (settings is not null)
            {
                _settings = settings.GetComponent<Settings>();
            }
        }
    }
}
