using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alertAttack : AttacksSystem
{
    [SerializeField]
    private GameObject atkPrefab;
    [SerializeField]
    private Vector3[] positions;
    private int posLength, posIndex;
    private float cooldown, timer;
    private Color color = Color.white;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        posLength = positions.Length;
        posIndex = UnityEngine.Random.Range(0, posLength);
        transform.position = positions[posIndex];
        cooldown = 1f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Blink());
    }
    private void Update() {
        timer += Time.deltaTime;
    }
    private void InstanAtk(){
        GameObject bencpress = Instantiate(atkPrefab, atkPrefab.transform.position, Quaternion.identity);
        BencpressMove bencpressMove= bencpress.GetComponent<BencpressMove>();
        bencpressMove.Started(positions[posIndex]);
        StartSelfDestruct(0f);
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
