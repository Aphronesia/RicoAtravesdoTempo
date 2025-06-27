using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greeneye : MonoBehaviour
{
    bool move;
    private Rigidbody2D rig;
    [SerializeField]
    private float speed, speedRotation;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        move = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (move){
            Moviment();
        }
        else{
            rig.velocity = new Vector2( 0, rig.velocity.y);
        }
        Rotation();
    }
    private void Moviment()
    {
        rig.velocity = new Vector2( -speed, rig.velocity.y);
    }
    private void Rotation()
    {
        transform.Rotate(0, 0, speedRotation * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            move = false;
        }
    }
}
