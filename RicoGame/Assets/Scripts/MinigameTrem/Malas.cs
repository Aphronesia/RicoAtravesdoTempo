using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malas : MonoBehaviour
{
    BoxCollider2D Collider;
    // Start is called before the first frame update
    void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Fundo"))
        {
            //Debug.Log("colidindo com fundo");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Fundo"))
        {
            //Debug.Log("colidindo com fundo");
            Destroy(gameObject);
        }
    }
}
