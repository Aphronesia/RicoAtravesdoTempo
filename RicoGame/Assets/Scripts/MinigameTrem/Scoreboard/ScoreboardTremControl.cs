using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MinigameTrem.Scoreboard;
using TMPro;
using UnityEngine;

public class ScoreboardTremControl : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TMP_InputField inputField;
    private Dictionary<string, int> scores = new Dictionary<string, int>();
    private const int MaxItems = 10;
    public ScoreManager scoreManager;
    public ScoreboardTremJson TremJson;
    // Start is called before the first frame update
    void Start()
    {
        //carrega o scoreboard salvo ou cria 1 item se nao tiver arquivo salvo
        LoadScores();
        UpdateScoreText();// Atualiza a visualizacao no TextMeshPro
    }
    //esse metodo eh chamado por um botao
    public void SubmitScore()
    {
        string playerName = inputField.text; //pega a string escrita no inputField
        string namePlayer = playerName.ToLower();//deixa todos os caracteres minusculos
        int points = 1; // scoreManager.points; //pega os pontos do script universo
        AddOrUpdateScore(namePlayer, points); 
    }
    // Adiciona um novo item, ou substitui o valor se ja existir
    public void AddOrUpdateScore(string name, int point)
    {
        if (scores.ContainsKey(name))
        {
            scores[name] = point; // Atualiza o valor se a chave ja existir
        }
        else
        {
            // Adiciona um novo item e remove o menor se ultrapassar o limite
            scores[name] = point;
            if (scores.Count > MaxItems)
            {
                RemoveLowestScore();
            }
        }
        UpdateScoreText(); // Atualiza o TextMeshPro com os novos dados
        SaveScores(); // Salva scores apos atualizacao
    }
    // Remove o item com o menor Score
    private void RemoveLowestScore()
    {
        var keyWithLowestScore = scores.Aggregate((x, y) => x.Value < y.Value ? x : y).Key;
        scores.Remove(keyWithLowestScore);
    }
    // Atualiza o TextMeshProUGUI com a lista de scores
    private void UpdateScoreText()
    {
        //GetSortedScores vai retornar a lista por ordem de pontuacao
        var sortedScores = GetSortedScores();
        scoreText.text = "";

        foreach (var points in sortedScores)
        {
            scoreText.text += $"{points.Key} - {points.Value} pontos\n";
        }
    }
    // Salva o Dictionary como um arquivo JSON
    private void SaveScores()
    {
        TremJson.scoresSave = scores; //atribui ao Dictionary do ScoreboardJson o Dictionary daqui
        TremJson.SaveScore();
    }
    // Carrega o Dictionary de um arquivo JSON
    private void LoadScores()
    {
        //chama a funcao LoadScore do TremJson
        //se nao existir arquivo a ser carregado retorna false
        bool tryload = TremJson.LoadScore();
        if(tryload)
        {
            scores = TremJson.scoresSave; //define ao Dictionary daqui o salvo
        }
        else
        {
            //Douglas sera um futuro inimimgo no jogo (ainda nao adicionado no momento)
            //coloquei isso pro scoreboard nao ficar vazio na primeira vez q jogar
            scores.Add("Douglas ", 35);
            SaveScores();
        }
    }
    // Retorna uma lista ordenada pelo maior Score
    public List<KeyValuePair<string, int>> GetSortedScores()
    {
        return scores.OrderByDescending(pair => pair.Value).ToList();
    }
}
