using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetoquecai : MonoBehaviour
{
    // Quando acontecer colisao
    void OnTriggerEnter2D(Collider2D collision)
    {
        // se a colisao for com um objeto com a tag "lava", se destruir
        if(collision.gameObject.tag=="Chao"){Destroy(this.gameObject);}
        // se a colisao for com um objeto com a tag "capivara", se destruir
        if (collision.gameObject.CompareTag("Capivara")){Destroy(gameObject);} 
    }
}
