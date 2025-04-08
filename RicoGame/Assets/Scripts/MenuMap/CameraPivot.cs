using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    [SerializeField]
    private GameObject ricopos;
    private Vector3 pos;
    private void Start() {
        ricopos = GameObject.Find("Rico");
    }
    private void Update(){
        pos = new Vector3(ricopos.transform.position.x, transform.position.y, transform.position.z);
        transform.position = pos;
    }
}
