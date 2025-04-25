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
    public int actualSceneIndex;
    private Vector3 pos;
    private CanvasGroup canvasGroup;   
    public List<LevelPop> levels = new List<LevelPop>();
    [System.Serializable]
    public class LevelPop{
        public string name;
        public string description;
        public int sceneIndex;
    }
    private void Start() {
        ricopos = GameObject.Find("Rico");
        if (canvasGroup == null){
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }
    private void Update() {
        pos = new Vector3 (ricopos.transform.position.x + distanceHorizontal, ricopos.transform.position.y, transform.position.z);
        transform.position = pos;
    }
    //chamado no RicoMove
    public void LevelEnter(int index){
        TextMeshProUGUI tmp = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup.alpha = Mathf.Clamp01(1f);
        actualSceneIndex = levels[index].sceneIndex;
        if (tmp != null){
            tmp.text = levels[index].name;
            //crasha caso nao exista um elemento no valor do index
        }
    }
    public void LevelExit(){
        canvasGroup.alpha = Mathf.Clamp01(0f);
    }
    public int SceneIndex(){
        return actualSceneIndex;
    }
}
