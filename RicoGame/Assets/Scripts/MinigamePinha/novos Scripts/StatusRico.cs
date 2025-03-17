using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusRico : MonoBehaviour
{
  
    public SpriteRenderer spriteRenderer;


  void Start()
    {
        // Obtém o componente SpriteRenderer do objeto atual
        spriteRenderer = GetComponent<SpriteRenderer>();
        
         if (spriteRenderer == null)
    {
        Debug.LogError("SpriteRenderer não encontrado no objeto.");
    }
       
    }

    // Método chamado quando ocorre uma colisão
    void OnCollisionEnter2D(Collision2D collision)
    {
     if(collision.gameObject.CompareTag("Bigorna"))
        StartCoroutine(DanoNumerator());

        if(collision.gameObject.CompareTag("Coracao"))
        StartCoroutine(Cura());
    }
    IEnumerator DanoNumerator()
    {
        spriteRenderer.color = Color.red;;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    IEnumerator Cura()
    {
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }



}
