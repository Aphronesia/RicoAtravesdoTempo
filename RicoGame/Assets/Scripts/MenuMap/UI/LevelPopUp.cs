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

    public bool cutscene;
    public int actualSceneIndex;
    public int actualCutsceneIndex;
    private Vector3 pos;
    private CanvasGroup canvasGroup;   
    
    private Image _image;
    public List<LevelPop> levels = new List<LevelPop>();
    [System.Serializable]
    public class LevelPop{
        public string name;
        public string description;
        public int sceneIndex;
        public bool cutscene;
        public int cutsceneIndex;
        public Sprite background;
    }
    private void Start()
    {
        TakeComponents();
        
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
        actualCutsceneIndex = levels[index].cutsceneIndex;
        cutscene = levels[index].cutscene;
        if (tmp != null){
            tmp.text = levels[index].name;
            _image.sprite = levels[index].background;
            //crasha caso nao exista um elemento no valor do index
        }
    }
    public void LevelExit(){
        canvasGroup.alpha = Mathf.Clamp01(0f);
    }

    public bool Cutscene(){
        return cutscene;
    }
    public int SceneIndex(){
        return actualSceneIndex;
    }

    public int ActualCutsceneIndex(){
        return actualCutsceneIndex;
    }

    private void TakeComponents()
    {
        ricopos = GameObject.Find("Rico");
        if (canvasGroup == null){
            canvasGroup = GetComponent<CanvasGroup>();
        }
        _image = GetComponent<Image>();
    }
}
