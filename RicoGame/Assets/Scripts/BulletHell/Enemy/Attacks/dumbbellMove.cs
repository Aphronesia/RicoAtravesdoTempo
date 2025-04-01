using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// primeiro ataque do guaxinim na primeira fase bulletHell
public class dumbbellMove : AttacksSystem
{
    [SerializeField, Tooltip("velocidade de movimentação")]
    private float speed;
    private bool direction, walk;
    private Animator anima;
    private void Start() {
        walk = false;
        //ChoseDirection();
        //StartSelfDestruct(4f);
        anima = GetComponent<Animator>();
    }
    private void FixedUpdate() {
        Moviment();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        switch (other.gameObject.tag){
            case "Player":
                Destroy(this.gameObject);
            break;
            case "Wall":
                StartCoroutine(CrashWall());
            break;
            case "Ground":
                walk = true;
                anima.SetBool("Grounded", true);
            break;
        }
    }
    private IEnumerator CrashWall(){
        anima.SetBool("Crash", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
    private void ChoseDirection(){
        direction = Random.value > 0.5f; // 50% de chance de ser true ou false

        // inverte ou não o sinal de speed de acordo com a direction
        speed = (direction ? 1 : -1) * Mathf.Abs(speed);

        // inverte a direção visual do objeto
        if (!direction){
            Vector3 scale = transform.localScale;
            scale.y *= -1;
            transform.localScale = scale;
            // A posição inicial do objecto é 
            // X:-4.25f 
            // Y:-3.4f
            // Z:0
            Vector3 spawnPosition = new Vector3(4.25f, -1.6f, 0);
            transform.position = spawnPosition;
        }
    }
    private void Moviment(){
        if (walk){
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
