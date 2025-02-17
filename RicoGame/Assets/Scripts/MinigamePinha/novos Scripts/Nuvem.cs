using System.Collections;
using UnityEngine;

public class nuvem : MonoBehaviour
{
    public GameObject nuvemPrefab; 

    public float minY = 2.54f, maxY = -1.59f,cloudVelocity = 5f, respawnDelay = 1f;
    

    
    void Start()
    {
        StartCoroutine(MoveCloud());
      
    }

    IEnumerator MoveCloud()
    {
        while (true)
        {
            transform.Translate(Vector3.right * cloudVelocity * Time.deltaTime);
            yield return null;
        }
     
     
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("mataNuvem")) // Verifica se colidiu com o objeto "mataNuvem"
        {
            Debug.Log("Nuvem destruida");
            Destroy(gameObject); // Destroi a nuvem atual
            StartCoroutine(CloudRespawn()); // Inicia o respawn
        }
    }

   
    IEnumerator CloudRespawn()
    {
        Debug.Log("Iniciando  respawn");
        yield return new WaitForSeconds(respawnDelay);
        CloudSpawn();

    }

    void CloudSpawn()
    {
        Debug.Log("Nuvem spawnada");
        // Gera uma posição aleatória no eixo Y dentro dos limites especificados
        float randomY = Random.Range(minY, maxY);

        // Define a posição de spawn 
        Vector3 spawnPosition = new Vector3(-7f, randomY, 0f);
        GameObject newCloud = Instantiate(nuvemPrefab,spawnPosition, Quaternion.identity);
        newCloud.AddComponent<nuvem>();

    }

}
