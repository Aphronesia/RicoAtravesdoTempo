using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class ScoreboardJson //esse codigo cria o txt do scoreboard da faze das pinhas
{
    public Dictionary<string, int> scoresSave; // Dictionary q sera salvo
    public string fileName = "Ricogame/Scoreboard.txt";
    
    public void SaveScore() //metodo de salvamento
    {
        try
        {
            // o scoresSave deve ter sido atribuido no script q chama esse metodo (ex o codigo ScoreboardControl)
            // converte o Dictionary scoresSave pra string no formato json
            // precisa da biblioteca using Newtonsoft.Json; pra funcionar
            string json = JsonConvert.SerializeObject(scoresSave, Formatting.Indented);

            // Atribui a documentsPath o caminho para a pasta Documentos do usuario
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Define o caminho completo para o novo arquivo
            string outputPath = Path.Combine(documentsPath, fileName);

            // Cria o arquivo de salvamento (caminho do arquivo, variavel a ser salva)
            File.WriteAllText(outputPath, json);

            Debug.Log("Arquivo salvo em: " + outputPath);
        }
        catch (Exception e) // se der erro 
        {
            Debug.LogError("Erro ao salvar o arquivo: " + e.Message);
        }
    }
    public void LoadScore()
    {
        try
        {
            // Atribui a documentsPath o caminho para a pasta Documentos do usuario
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // Define o caminho completo para o novo arquivo
            string filePath = Path.Combine(documentsPath, fileName);
            //verifica se o arquivo de salvamento fileName ja existe
            if (File.Exists(filePath))
            {
                //atribui a essa string o que esta no arquivo de salvamento
                string json = File.ReadAllText(filePath);
                //converte a string a um Dictionary e a atribui ao string scoresSave
                scoresSave = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
            }
            else
            {
                Debug.LogWarning("Arquivo não encontrado: " + filePath);
            }
        }
        catch (Exception e) // se der erro 
        {
            Debug.LogError("Erro ao carregar o arquivo: " + e.Message);
            SaveScore();
        }
    }
}
