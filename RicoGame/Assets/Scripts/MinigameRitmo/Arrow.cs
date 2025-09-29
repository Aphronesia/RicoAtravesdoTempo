using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speedDown;
    private Rigidbody2D rb;

    private void Start()
    {
        TakeComponest();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, -speedDown);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Bigorna":
                Destroy(gameObject);
                break;
            default:
                Destroy(gameObject);
                break;
                
            
        }
    }

    private void TakeComponest()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
