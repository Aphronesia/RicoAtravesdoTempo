using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public SettingsJson settingsJson;
    public float volume;
    
    public void Save(float indexVolume)
    {
        settingsJson.volume = indexVolume;
        settingsJson.SaveSettings();
    }
    public void Load()
    {
        settingsJson.loadSettings();
        volume = settingsJson.volume;
    }
}
