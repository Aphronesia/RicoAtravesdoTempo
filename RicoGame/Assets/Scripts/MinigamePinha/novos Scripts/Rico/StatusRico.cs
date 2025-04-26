using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusRico : MonoBehaviour
{
  
    public SpriteRenderer spriteRenderer;
    public Color colorNormal = Color.white;
    public Color colorDamage = Color.red;
    public Color colorHealh = Color.green;
     public Temporizador temporizador; 

    

    private Rico ricoScript;
    [SerializeField] private int healAmount = 1;
    public int pineCones;

    [SerializeField]
    private TMP_Text uiPinhas;
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
             if (temporizador != null)
            {
                temporizador.Dano(); // Chama o método de dano (que tira 10 segundos)
            }
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
            uiPinhas.text = pineCones.ToString(pineCones >= 100 ? "D3" : "D2");
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
