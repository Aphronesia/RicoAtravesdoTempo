using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Rico : MonoBehaviour
{
      public Vector3[] targetPositions; //array das cordenadas 
    public float speed = 5f; 
    private int currentTargetIndex = 0, direction = 1;  // 1 para direita, -1 para esquerda
    private bool isMoving = false, turn = true; 
    private Animator anima;


    void Start()
    {
        anima = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!isMoving && currentTargetIndex < targetPositions.Length - 1)
            {
                direction = 1; 
                isMoving = true; 
                Flip(false);
                currentTargetIndex++; // Avança para a próxima coordenada

            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!isMoving && currentTargetIndex > 0)
            {
                direction = -1; 
                isMoving = true; 
                 Flip(true);
                currentTargetIndex--; // Retrocede para a coordenada anterior
            }
        }

        // Move o objeto se estiver em movimento
        if (isMoving)
        {
            MoverRico();
            anima.SetBool("CanRoll",true);
        }
        else
        {
            anima.SetBool("CanRoll",false);
        }
    }

    void MoverRico()
    {
        // Move o rico direção à coordenada atual
        transform.position = Vector3.MoveTowards(transform.position, targetPositions[currentTargetIndex], speed * Time.deltaTime);

        // Verifica se chegou na coordenada
        if (transform.position == targetPositions[currentTargetIndex])
        {
            Debug.Log("rico pousou no " + currentTargetIndex);
            isMoving = false; // Para o movimento
        }
    }
    void Flip(bool flipToRight)
    {
        if((flipToRight && !turn) || (!flipToRight && turn))
        {
            turn = !turn;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

    }
}