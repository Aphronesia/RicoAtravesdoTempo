using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class TopScoresData
{
    public List<int> scores = new List<int>();
}

public class ScoreBoard : MonoBehaviour
{
    private List<int> topValues = new List<int>();
    public int maxSize = 10;

    private string filePath;

    private void Awake()
    {
        // Caminho da pasta AppData local (ex: C:\Users\<user>\AppData\LocalLow\<Company>\<Game>)
        filePath = Path.Combine(Application.persistentDataPath, "TopScores.json");

        LoadFromJson();
    }

    public void AddValue(int newValue)
    {
        topValues.Add(newValue);
        topValues = topValues.OrderByDescending(v => v).ToList();

        if (topValues.Count > maxSize)
            topValues.RemoveAt(topValues.Count - 1);

        SaveToJson();

        //Debug.Log("Ranking atual: " + string.Join(", ", topValues));
    }

    public List<int> GetTopValues()
    {
        return new List<int>(topValues);
    }

    private void SaveToJson()
    {
        TopScoresData data = new TopScoresData { scores = topValues };
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        Debug.Log($"Ranking salvo em: {filePath}");
    }

    private void LoadFromJson()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            TopScoresData data = JsonUtility.FromJson<TopScoresData>(json);
            topValues = data.scores ?? new List<int>();
            Debug.Log("Ranking carregado: " + string.Join(", ", topValues));
        }
        else
        {
            topValues = new List<int>();
            Debug.Log("Nenhum arquivo de ranking encontrado, criando novo...");
        }
    }
}