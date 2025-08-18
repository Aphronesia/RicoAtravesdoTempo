using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alertAttack : AttacksSystem
{
    [SerializeField]
    private GameObject atkPrefab;
    [SerializeField]
    private Vector3[] positions;
    private int posIndex;
    private float cooldown, timer;
    private Color color = Color.white;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        //posIndex = UnityEngine.Random.Range(0, posLength);
        //transform.position = positions[posIndex];
        
        Spawn();
        cooldown = 1f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Blink());
    }
    private void Spawn(){
        GameObject player = GameObject.Find("Rico");
        if (player == null){
            return;
        }
        float posPlayer = player.transform.position.x;
        float disPosition = float.MaxValue;
        int posLength = positions.Length;

        for (int i = 0; i < posLength; i++){
            float difference = Mathf.Abs(positions[i].x - posPlayer);
            if(difference <= disPosition){
                disPosition = difference;
                posIndex = i;
            }  
        }
        transform.position = positions[posIndex];
    }
    private void Update() {
        timer += Time.deltaTime;
    }
    private void InstanAtk(){
        GameObject bencpress = Instantiate(atkPrefab, atkPrefab.transform.position, Quaternion.identity);
        BencpressMove bencpressMove= bencpress.GetComponent<BencpressMove>();
        bencpressMove.Started(positions[posIndex]);
        StartSelfDestruct(0f, gameObject);
    }
    private IEnumerator Blink(){
        do{
            yield return new WaitForSeconds(0.05f);
            color.a = 0f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.05f);
            color.a = 1f;
            spriteRenderer.color = color;
        } while (timer < cooldown);
        InstanAtk();
    }
}
