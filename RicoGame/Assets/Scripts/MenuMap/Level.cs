using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.gameObject.tag){
            case "Player":
                Debug.Log("foi");
            break;
        }
    }
}
