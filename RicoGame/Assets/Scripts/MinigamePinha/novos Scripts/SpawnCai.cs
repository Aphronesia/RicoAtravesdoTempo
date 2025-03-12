using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCai : MonoBehaviour
{
    [SerializeField]
    private GameObject Pinha, Bigorna,Coracao;
    
    // Array de posições possíveis para spawn
    float[] possivelposicao = {-1.75f, 0f, 1.75f};

    // Taxa de spawn (intervalo entre spawns)
    private float SpawnRate = 2f;

    // Variável para controlar se o spawn está ativo
    public bool PodeSpawn = true;

    // Array de índices para spawn (pode ser ajustado no Inspector)
    int[] Spawnobjeto = {1, 2, 3, 4, 5, 6, 7, 8};
    public void TempoPadrao()
    {
        SpawnRate = 1f;
    }
    public void TempoRapido()
    {
        SpawnRate = 0.5f;
    }
    public void TempoMedio()
    {
        SpawnRate = 0.75f;
    }
    public void Tempofim()
    
   {
    PodeSpawn = false;
   }

    // Coroutine para gerar os objetos
    IEnumerator GeracaoComida()
    {
        while (PodeSpawn)
        {
            // Obtenha um índice aleatório entre 0 e o comprimento do vetor menos 1
            int indiceAleatorio = UnityEngine.Random.Range(0, possivelposicao.Length);
            int indiceSpawn = UnityEngine.Random.Range(0, Spawnobjeto.Length);
            int Spawn = Spawnobjeto[indiceSpawn];
            float posicaoaleatoria = possivelposicao[indiceAleatorio];

            // Escolha o objeto para spawnar com base no valor de Spawn
            switch (Spawn)
            {
                case <= 5:
                    Instantiate(Pinha, new Vector3(posicaoaleatoria, 6, 0), Quaternion.identity);
                    break;
                case > 5 and <= 7:
                    Instantiate(Bigorna, new Vector3(posicaoaleatoria, 6, 0), Quaternion.identity);
                    break;
                case >= 8:
                    Instantiate(Coracao, new Vector3(posicaoaleatoria, 6, 0), Quaternion.identity);
                    break;
            }

            // Aguarde o tempo definido em SpawnRate antes de spawnar o próximo objeto
            yield return new WaitForSeconds(SpawnRate);
        }
    }

    // Inicie a coroutine quando o jogo começar
    void Start()
    {
        StartCoroutine(GeracaoComida());
    }
    }

