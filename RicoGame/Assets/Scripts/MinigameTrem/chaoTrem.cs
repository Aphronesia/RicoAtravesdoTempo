using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaoTrem : MonoBehaviour
{
    public Transform posicaoRico;
    void Update()
    {
        Movimento();
    }
    void Movimento()
    {
        Vector3 novaPosicao = transform.position;
        novaPosicao.x = posicaoRico.position.x;
        transform.position = novaPosicao;

    }
}
