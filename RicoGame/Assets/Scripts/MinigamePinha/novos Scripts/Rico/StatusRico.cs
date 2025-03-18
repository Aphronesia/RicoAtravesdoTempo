using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusRico : MonoBehaviour
{
    public int vida;
    public SpriteRenderer spriteRenderer;
    public Color colorNormal = Color.white;
    public Color colorDamage = Color.red;
    public Color colorHealh = Color.green;


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
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Bigorna"))
        {
            Destroy(other.gameObject);
            StartCoroutine(DanoNumerator());
        }

        if (other.gameObject.CompareTag("Coracao"))
        {
            Destroy(other.gameObject);
            StartCoroutine(Cura());
        }
        //Debug.Log("TRIGGER: coligiu com: " + other.gameObject.tag);
    }
    IEnumerator DanoNumerator()
    {
        spriteRenderer.color = colorDamage;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = colorNormal;
    }

    IEnumerator Cura()
    {
        spriteRenderer.color = colorHealh;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = colorNormal;
    }



}
