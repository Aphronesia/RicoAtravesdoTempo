using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour
{
    // Quando acontecer colisao
    void OnCollisionEnter2D(Collision2D collision)
    {
        // se a colisao for com um objeto com a tag "lava", se destruir
        if(collision.gameObject.tag=="Chao"){Destroy(this.gameObject);}
        // se a colisao for com um objeto com a tag "capivara", se destruir
        if (collision.gameObject.CompareTag("Capivara")){Destroy(gameObject);} 
    }
}
