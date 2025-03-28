using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [Header("Atributos da movimentação")]
    [SerializeField, Tooltip("velocidade do rico")]
    private float speed;
    [SerializeField, Tooltip("Força do pulo")]
    private float jumpForce;
    private bool isGrounded = false;
    private Rigidbody2D rig;
    Vector2 moveDirection = Vector2.zero;

    public Joystick fixedjoy; 
    private void Start() {
        rig = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        Moviment();
    }
    private void Moviment(){
        rig.velocity = new Vector2(fixedjoy.Horizontal * speed, rig.velocity.y);
    }
    public void Jump(){
        if (isGrounded){
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        switch (other.gameObject.tag)
        {
            case "Ground":
                isGrounded = true;
            break;
            default:
            break;
        }
    }
}
