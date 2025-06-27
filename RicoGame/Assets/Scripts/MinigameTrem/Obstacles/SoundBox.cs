using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBox : MonoBehaviour
{
    public float impulso;
    void Start()
    {
        
    }
    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Fundo"))
        {
            //Debug.Log("colidindo com fundo");
            Destroy(gameObject);
        }
        if(colisao.gameObject.CompareTag("Player")){
            colisao.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * impulso, ForceMode2D.Impulse);
        }
        //Debug.Log("colidindo com algo");
    }
}
