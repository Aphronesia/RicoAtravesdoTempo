using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BencpressMove : AttacksSystem
{
    public void Started(Vector3 pos){
        pos.y = 0;
        transform.position = pos;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        switch (other.gameObject.tag){
            case "Player":
                Destroy(this.gameObject);
            break;
            case "Ground":
                Destroy(this.gameObject);
            break;
        }
    }
}
