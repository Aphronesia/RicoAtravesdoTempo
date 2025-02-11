using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SettingsJson
{
    public float volume;
    bool exist;
    private string path = "Assets/Settings.txt";

    public void SaveSettings()
    {
        var content = JsonUtility.ToJson(this, true);
        File.WriteAllText(path, content);
    }
    public void loadSettings()
    {
        exist = File.Exists(path);
        //Debug.Log("exist = " + exist);
        if (exist)
        {
            var content = File.ReadAllText(path);
            var s = JsonUtility.FromJson<SettingsJson>(content);

            volume = s.volume;
        }
        else
        {
            volume = 0.1f;
            var content = JsonUtility.ToJson(this, true);
            File.WriteAllText(path, content);
        }
        
    }
}
