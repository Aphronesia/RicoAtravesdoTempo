using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPopUp : MonoBehaviour
{
    [SerializeField]
    private GameObject ricopos;
    [SerializeField]
    private float distanceHorizontal;
    [SerializeField]
    private string levelname;
    private Vector3 pos;    
    public List<LevelPop> levels = new List<LevelPop>();
    [System.Serializable]
    public class LevelPop{
        public string name;
        public string description;
        public int sceneIndex;
    }
    private void Start() {
        ricopos = GameObject.Find("Rico");
    }
    private void Update() {
        pos = new Vector3 (ricopos.transform.position.x + distanceHorizontal, ricopos.transform.position.y, transform.position.z);
        transform.position = pos;
    }
    public void LevelEnter(int index){
        TextMeshProUGUI tmp = GetComponentInChildren<TextMeshProUGUI>();
        if (tmp != null){
            tmp.text = levels[index].name;
            //crasha caso nao exista um elemento no valor do index
        }
    }
    public void LevelExit(){
        //zerar a opacidade
    }
}
