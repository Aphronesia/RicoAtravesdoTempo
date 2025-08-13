using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

namespace Game 
{
    public class ControlSounds : MonoBehaviour
    {   
        [Header("Volumes")]
        [Range(0, 1)] public float volumeMusic;
        [Range(0, 1)] public float volumeSFX;
        
        [Header("Sounds")]
        [SerializeField]
        private List<Audio> musics = new List<Audio>();
        [SerializeField]
        private List<Audio> soundEffects = new List<Audio>();

        private int _actualMusic;
        private int _actualSFX;
        
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
        private void Awake()
        {
            BecomeIndestructible();
            
        }
        private void BecomeIndestructible()
        {
            if (FindObjectOfType<ControlSounds>() != null) 
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

        private void PlayMusic(string clipName)
        {
            int index = musics.FindIndex(x => x.name == clipName);
            if (index == _actualMusic || index == -1)
            {
                return;
            }
            musicSource.Stop();
            musicSource.clip = musics[index].clip;
            musicSource.Play();
            _actualMusic = index;
            
        }

        private void ChangeVolumeMusic()
        {
            volumeMusic = _settings.volumeMusic;
        }

        private void ChangeVolumeSFX()
        {
            volumeSFX = _settings.volumeMaster;
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
