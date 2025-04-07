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
    public bool isGrounded = false;
    private Rigidbody2D rig;
    public  SpriteRenderer spriteRenderer;
    private Animator animator;
    Vector2 moveDirection = Vector2.zero;

    public Joystick fixedjoy; 
    private void Start() {
        rig = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate() {
        Moviment();
    }
    private void Moviment(){
        rig.velocity = new Vector2(fixedjoy.Horizontal * speed, rig.velocity.y);
        if (fixedjoy.Horizontal >= 0.1f){
            spriteRenderer.flipX = true;
        }
        if (fixedjoy.Horizontal <= -0.1f){
            spriteRenderer.flipX = false;
        }
        animator.SetBool("Move", rig.velocity.x != 0);
    }
    public void Jump(){
        if (isGrounded){
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetTrigger("jump");
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
        //Debug.Log("colidiu com: " + other.gameObject.tag);
    }
}
