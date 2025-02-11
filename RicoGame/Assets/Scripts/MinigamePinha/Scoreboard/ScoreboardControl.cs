using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Newtonsoft.Json;
using UnityEngine;

public class ScoreboardControl : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TMP_InputField inputField;
    private Dictionary<string, int> scores = new Dictionary<string, int>();
    private const int MaxItems = 10;
    public Universo universo;
    public ScoreboardJson scoreboardJson;
    private void Start()
    {
        // XD - Vampy (from twitter @vampyyXD) 2024                                  (Easter egg kk)
        //carrega o scoreboard salvo ou cria 1 item se nao tiver arquivo salvo
        LoadScores();

        UpdateScoreText(); // Atualiza a visualizacao no TextMeshPro
    }
    //metodo que envia a string name e int pinha pro metodo AddOrUpdateScore
    //esse metodo eh chamado por um botao
    public void SubmitScore()
    {
        string playerName = "Player" ;
        if (inputField.text != "")
        {
            playerName = inputField.text; //pega a string escrita no inputField se nao for nula
        }
        string namePlayer = playerName.ToLower();//deixa todos os caracteres minusculos
        int pinhas = universo.pontos; //pega os pontos do script universo
        AddOrUpdateScore(namePlayer, pinhas); 
    }
    // Adiciona um novo item, ou substitui o valor se ja existir
    public void AddOrUpdateScore(string name, int pinha)
    {
        if (scores.ContainsKey(name))
        {
            scores[name] = pinha; // Atualiza o valor se a chave ja existir
        }
        else
        {
            // Adiciona um novo item e remove o menor se ultrapassar o limite
            scores[name] = pinha;
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
    public void UpdateScoreText()
    {
        //GetSortedScores vai retornar a lista por ordem de pontuacao
        var sortedScores = GetSortedScores();
        scoreText.text = "";

        foreach (var pinha in sortedScores)
        {
            scoreText.text += $"{pinha.Key} - {pinha.Value} pinhas\n";
        }
    }
    // Salva o Dictionary como um arquivo JSON
    private void SaveScores()
    {
        scoreboardJson.scoresSave = scores; //atribui ao Dictionary do ScoreboardJson o Dictionary daqui
        scoreboardJson.SaveScore();
    }
    // Carrega o Dictionary de um arquivo JSON
    private void LoadScores()
    {
        //chama a funcao LoadScore do ScoreboardJson
        //se nao existir arquivo a ser carregado retorna false
        scoreboardJson.LoadScore();
        scores = scoreboardJson.scoresSave; //define ao Dictionary daqui o salvo
        UpdateScoreText(); // Atualiza a visualizacao no TextMeshPro
    }
    // Retorna uma lista ordenada pelo maior Score
    public List<KeyValuePair<string, int>> GetSortedScores()
    {
        return scores.OrderByDescending(pair => pair.Value).ToList();
    }
}
