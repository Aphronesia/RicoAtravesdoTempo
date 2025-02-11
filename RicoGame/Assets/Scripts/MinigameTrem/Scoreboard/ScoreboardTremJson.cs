using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class ScoreboardTremJson
{
    public Dictionary<string, int> scoresSave; //
    private string path = "Assets/Saves/ScoreboardTrem.txt"; //arquivo de salvamento
    
    public void SaveScore() //metodo de salvamento
    {
        // o scoresSave deve ter sido atribuido no script q chama esse metodo (ex o codigo ScoreboardControl)
        // converte o Dictionary scoresSave pra string no formato json
        // precisa da biblioteca using Newtonsoft.Json; pra funcionar
        string json = JsonConvert.SerializeObject(scoresSave, Formatting.Indented);
        File.WriteAllText(path, json); //cria ou sobrescreve o arquivo path
    }
    public bool LoadScore()
    {
        //verifica se o arquivo de salvamento path ja existe
        if (File.Exists(path))
        {
            //atribui a essa string o que esta no arquivo de salvamento
            string json = File.ReadAllText(path);
            //converte a string a um Dictionary e a atribui ao string scoresSave
            scoresSave = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
            return true;
        }
        else
        {
            Debug.LogWarning($"arquivo de salvamento nao existe");
            return false;
        }
    }
}
