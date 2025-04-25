using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [Header("Atributos da movimentação")]
    [SerializeField, Tooltip("velocidade do rico")]
    private float speed;
    [SerializeField, Tooltip("Força do pulo")]
    private float jumpForce; //9
    public bool isGrounded = false;
    private Rigidbody2D rig;
    public  SpriteRenderer spriteRenderer;
    private Animator animator;
    Vector2 moveDirection = Vector2.zero;

    public Joystick fixedjoy;
    private Vector2 vecMove; 
    private void Start() {
        rig = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate() {
        Moviment();
        Keyboard();
    }
    private void Keyboard(){
        vecMove.x = Input.GetAxis("Horizontal");
    }
    private void Moviment(){
        rig.velocity = new Vector2((fixedjoy.Horizontal + vecMove.x)* speed, rig.velocity.y);
        if (fixedjoy.Horizontal  >= 0.1f || vecMove.x >= 0.1f){
            spriteRenderer.flipX = true;
        }
        if (fixedjoy.Horizontal <= -0.1f || vecMove.x <= -0.1f){
            spriteRenderer.flipX = false;
        }
        animator.SetBool("Move", rig.velocity.x != 0 && isGrounded);
        animator.SetBool("Jump", rig.velocity.y >= 0.1);
        
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
                animator.SetTrigger("Ground");
            break;
            default:
            break;
        }
        //Debug.Log("colidiu com: " + other.gameObject.tag);
    }
}
