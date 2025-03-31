using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNuvem : MonoBehaviour
{
    [SerializeField]
    private GameObject NuvemPrefab; // Prefab da nuvem
    [SerializeField]

    private GameObject Nuvem2_Prefab;
    private float escalaRandom; // Variável para armazenar a escala aleatória

    void Start()
    {
        // Inicia a corrotina para gerar nuvens
        StartCoroutine(GeracaoNuvem());

        // Gera uma escala aleatória para as nuvens
        escalaRandom = Random.Range(0.5f, 2.0f); // Escala entre 0.5 e 2.0
        Vector3 escala = new Vector3(escalaRandom, escalaRandom, escalaRandom);
    }

    IEnumerator GeracaoNuvem()
    {
        while (true) // Loop infinito para gerar nuvens
        {
            // Gera uma posição Y aleatória para a nuvem
            float posicaoY = Random.Range(-1.59f, 2.54f);

            // Instancia a nuvem na posição (-4.6, posicaoY, 0) com rotação padrão
            Instantiate(NuvemPrefab, new Vector3(-9.5f, posicaoY, 0f), Quaternion.identity);

            // Espera 3 segundos antes de gerar a próxima nuvem
            yield return new WaitForSeconds(2);

            Instantiate(Nuvem2_Prefab, new Vector3(-9.5f, posicaoY, 0f), Quaternion.identity);

            // Espera 5 segundos antes de gerar a próxima nuvem
            yield return new WaitForSeconds(5);
        }
    }
}