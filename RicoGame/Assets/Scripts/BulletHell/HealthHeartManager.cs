using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public Player_Status player_Status;
    List<HealthHeart> hearts = new List<HealthHeart>();

    private void OnEnable() {
        Player_Status.OnPlayerDamaged += DrawHearts;
    } 
    private void OnDisable() {
        Player_Status.OnPlayerDamaged -= DrawHearts;
    }
    private void Start() {
        DrawHearts();
    }
    public void DrawHearts(){
        ClearHearts(); 
        //determinar quantos corações fazer no total
        //baseado na vida total (healthMax)
        float maxHealthRemainder = player_Status.healthMax % 2;
        int heartsToMake = (int)((player_Status.healthMax / 2) + maxHealthRemainder);
        for (int i = 0; i < heartsToMake; i++){
            CreateEmptyHeart();
        }
        for (int i = 0; i < hearts.Count; i++ ){
            int heartStatusRemainder = (int)Mathf.Clamp(player_Status.health - ( i*2 ), 0, 2 );
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }
    public void CreateEmptyHeart(){
        //Instancea o prefab
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        //define o pai do Transform
        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        
        //deixa o coração vazio (Empty)
        heartComponent.SetHeartImage(HeartStatus.Empty);
        
        //adiciona a lista
        hearts.Add(heartComponent); 
    }
    public void ClearHearts(){
        foreach (Transform t in transform){
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }
}
