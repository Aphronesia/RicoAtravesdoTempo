using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estrelas : MonoBehaviour
{
    public bool canMove;
    public Vector3 Posicao;
    public float final;
    public float veloc = -5.0f;
    public Vector2 direcao = Vector2.right;
    public Transform posicaoRico;
    // Update is called once per frame
    private void Start() {
        canMove = false;
    }
    void Update()
    {
        if(canMove){
            Movimento();
        }
        
    }
    void Movimento()
    {
        Vector3 movimento = direcao * veloc * Time.deltaTime;
        transform.position += movimento;
        if(transform.position.x > final){
        movimento = direcao * veloc * Time.deltaTime;
        transform.position += movimento;
        }
        else if(transform.position.x < final){
            transform.position = Posicao;
        }
    }
}
