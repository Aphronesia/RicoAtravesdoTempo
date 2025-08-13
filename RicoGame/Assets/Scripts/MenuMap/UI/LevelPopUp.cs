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
    [SerializeField] private CanvasGroup cgBack;
    
    [SerializeField]
    private TextMeshProUGUI _titleText;
    [SerializeField]
    private TextMeshProUGUI _descriptionText;
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
    private void Update()
    {
        //Moviment();
    }

    private void Moviment()
    {
        pos = new Vector3 (ricopos.transform.position.x + distanceHorizontal, ricopos.transform.position.y, transform.position.z);
        transform.position = pos;
    }
    //chamado no RicoMove
    public void LevelEnter(int index){
        //TextMeshProUGUI tmp = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup.alpha = Mathf.Clamp01(1f);
        cgBack.alpha = Mathf.Clamp01(1f);
        actualSceneIndex = levels[index].sceneIndex;
        actualCutsceneIndex = levels[index].cutsceneIndex;
        cutscene = levels[index].cutscene;
        
        _titleText.text = levels[index].name;
        _descriptionText.text = levels[index].description;
        _image.sprite = levels[index].background;
    }
    public void LevelExit(){
        canvasGroup.alpha = Mathf.Clamp01(1f);
        cgBack.alpha = Mathf.Clamp01(0f);
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
