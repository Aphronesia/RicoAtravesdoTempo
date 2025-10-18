using System;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace MinigameRitmo
{
    public class MusicControl : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] 
        private float _volume;
        [Header("Musicas")]
        [SerializeField]
        private List<Audio> musics = new List<Game.Audio>();

        [SerializeField, Tooltip("nome da musica já que so tem uma no jogo por enquanto")]
        private string musicName;
        private Audio _actualMusic;
        [SerializeField]
        private AudioSource _audioSource;
        
        // Componentes  
        private Settings _settings;
        private ControlScenes _controlScenes;
        
        
        private void OnEnable()
        {
            ControlSounds.OnChangeVolumes += ChangeVolume;
            UIGeral.ConfigUI.OnPause += PauseMusic;
        }

        private void OnDisable()
        {
            ControlSounds.OnChangeVolumes -= ChangeVolume;
            UIGeral.ConfigUI.OnPause -= PauseMusic;
        }

        private void ChangeVolume()
        {
            _volume = _settings.volumeMusic;
            _audioSource.volume = _volume;
        }

        private void Start()
        {
            TakeComponents();
            ChangeVolume();
        }

        public void PlayMusic()
        {
            string clipName = musicName;
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
            _audioSource.Stop();
            _audioSource.clip = music.clip;
            _audioSource.Play();
            _actualMusic =  music;
        }

        private void PauseMusic(bool pause)
        {
            if (pause)
                _audioSource.Pause();
            else
                _audioSource.UnPause();
        }
        private void TakeComponents()
        {
            _audioSource = GetComponent<AudioSource>();
            var settings = GameObject.Find("GameSettings");
            if (settings != null)
            {
                _settings = settings.GetComponent<Settings>();
            }
            ControlScenes scenesController = FindObjectOfType<ControlScenes>();
            if (scenesController != null){
                _controlScenes = scenesController.GetComponent<ControlScenes>();
            }
        }
    }
}
