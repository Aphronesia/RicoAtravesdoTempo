using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusRico : MonoBehaviour
{
    [Header("atributos rico")]
    [SerializeField,Tooltip("Cores colisão")]
    private SpriteRenderer spriteRenderer;
    private Color colorNormal = Color.white;
    private Color colorDamage = Color.red;
    private Color colorHealh = Color.green;

    [SerializeField,Tooltip("vida")]
    private int vida;
    private Sprite deadRico;
    float impulso;


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
