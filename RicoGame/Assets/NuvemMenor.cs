using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuvemMenor : MonoBehaviour
{
  public GameObject nuvemPrefab; 

    public float cloudVelocity = 2f;
    

    
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
             
        }
    }

   

}
