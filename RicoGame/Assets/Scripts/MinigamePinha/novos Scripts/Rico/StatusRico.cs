using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusRico : MonoBehaviour
{
  
    public SpriteRenderer spriteRenderer;
    public Color colorNormal = Color.white;
    public Color colorDamage = Color.red;
    public Color colorHealh = Color.green;

    private Rico ricoScript;
    [SerializeField] private int healAmount = 1;

    private void Start()
    {
       ricoScript = GetComponent<Rico>();
       if(ricoScript==null){
        Debug.LogError("Script rico n encontrado");
       }
    }

    // Método chamado quando ocorre uma colisão
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Bigorna"))
        {
            Destroy(other.gameObject);
            StartCoroutine(DanoNumerator());
             if (ricoScript != null){
                 ricoScript.Damage();
             }
           
            
        }

        if (other.gameObject.CompareTag("Coracao"))
        {
            Destroy(other.gameObject);
            StartCoroutine(Cura());
            if (ricoScript != null)
        {
            //Debug.Log("Chamando método Heal");
            ricoScript.Heal(healAmount);
        }
        
        }
       
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
