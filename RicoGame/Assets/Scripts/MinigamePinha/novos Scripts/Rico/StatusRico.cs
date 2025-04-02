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
    public int pineCones;

    private void Start()
    {
       ricoScript = GetComponent<Rico>();
      
       if(ricoScript==null){
        Debug.LogError("Script rico n encontrado");
       }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Bigorna")){
            Destroy(other.gameObject);
            StartCoroutine(DamageNumerator());
             if (ricoScript != null){
                 ricoScript.Damage();
             }  
        }

        if (other.gameObject.CompareTag("Coracao")){
            Destroy(other.gameObject);
            StartCoroutine(Cura());
            if (ricoScript != null){
                ricoScript.Heal(healAmount);
            }
        }
        if (other.gameObject.CompareTag("Pinha")){
             Destroy(other.gameObject);
            pineCones++;
        // Atualiza o score quando pegar uma pinha
    
        }
       
    }
    IEnumerator DamageNumerator()
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
