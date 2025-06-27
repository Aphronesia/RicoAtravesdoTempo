using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstros : MonoBehaviour
{
    //private float veloc = 5.8f;
    public bool canWalk;
    private Animator anima;
    private Rigidbody2D rig;
    public Vector3 posicalTeleporte;
    [SerializeField]
    private Transform PosicaoRico;

    void Start()
    {
        anima = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        canWalk = false;
        anima.SetBool("Walk", false);
    }
    void FixedUpdate()
    {
        Moviment();
    }
    public void StartGame()
    {
        anima.SetBool("Walk", true);
    }
    public void RicoDead()
    {
        anima.SetBool("Walk", false);
    }
    void Moviment()
    {
        Vector3 novaPosicao = transform.position;
        novaPosicao.x = PosicaoRico.position.x - 1.9f;
        transform.position = novaPosicao;
    }
    //teleporta de volta pro inici do trem igual faz com o rico
    public void Teleporte()
    {
        Debug.Log("tenia teleportou");
        posicalTeleporte = new Vector3(transform.position.x - PosicaoRico.position.x, transform.position.y, 0);
        transform.position = posicalTeleporte;
    }
}
