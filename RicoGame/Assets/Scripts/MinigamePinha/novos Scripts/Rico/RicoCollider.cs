using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicoCollider : MonoBehaviour
{
    //código teste pra testar colissão pq tava bugando mt 
    //DELETAR DEPOIS
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Bigorna")){
            //Debug.Log("oioioi");
        }
        //Debug.Log("COLLISION: coligiu com: " + other.gameObject.tag);
    }
}
