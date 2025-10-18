using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControllerTrem : MonoBehaviour
{
    //Componente q executa o som e os sons
    public AudioSource audioSource;
    public AudioClip[] sounds;
    public Slider slider;
    public float volumeLoaded;

    private void OnEnable()
    {
        Game.Settings.OnLoadSettings += AtualizasSom;
    }

    private void OnDisable()
    {
        Game.Settings.OnLoadSettings -= AtualizasSom;
    }

    private void AtualizasSom()
    {
        volumeLoaded = slider.value;
        OnSliderValueChanged(volumeLoaded);
    }
    public void PlaySound(int index)
    {
        audioSource.Stop();
        //verifica se o indice esta nos limites do array
        if (index >= 0 && index < sounds.Length)
        {
            audioSource.clip = sounds[index]; //define o clip correspondente
            audioSource.Play(); //toca o som
        }
        else
        {
            Debug.LogWarning($"Indice fora dos limites do array de sons.");
        }
    }
    void OnSliderValueChanged(float value)
    {
        // Chame a função que precisa do valor float aqui
        ChangeVolume(value);
    }
    //o valor recebido deve ter entre 0.0 e 1.0
    public void ChangeVolume(float volume)
    {
        //garante q o valor esta entre 0.0 e 1.0 
        //se menor q 0.0 volume == 0.0 
        //se maior q 1.0 volume == 1.0 
        volume = Mathf.Clamp(volume, 0.0f, 1.0f);
        //altera o volume
        audioSource.volume = volume;
    }
}
