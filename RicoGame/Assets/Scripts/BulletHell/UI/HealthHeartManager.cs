using System.Collections;
using System.Collections.Generic;
using BulletHell.Player;
using Game.Player;
using UnityEngine;

public class HealthHeartManager : MonoBehaviour
{
    public GameObject Rico;
    public GameObject heartPrefab;
    public IPlayer_Status player_Status;
    List<HealthHeart> hearts = new List<HealthHeart>();

    private void OnEnable() {
        Player_Status.OnPlayerDamaged += DrawHearts;
    } 
    private void OnDisable() {
        Player_Status.OnPlayerDamaged -= DrawHearts;
    }
    private void Start() {
        TakeComponents();
        if (player_Status == null) {
            Debug.LogError("fudeu");
        }
        DrawHearts();
    }
    public void DrawHearts(){
        ClearHearts(); 
        //determinar quantos corações fazer no total
        //baseado na vida total (healthMax)
        float maxHealthRemainder = player_Status.IhealthMax % 2;
        int heartsToMake = (int)((player_Status.IhealthMax / 2) + maxHealthRemainder);
        for (int i = 0; i < heartsToMake; i++){
            CreateEmptyHeart();
        }
        for (int i = 0; i < hearts.Count; i++ ){
            int heartStatusRemainder = (int)Mathf.Clamp(player_Status.Ihealth - ( i*2 ), 0, 2 );
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }
    public void CreateEmptyHeart(){
        //Instancea o prefab
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform, false);

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

    private void TakeComponents() {
        if (Rico is null) {
            Rico = GameObject.Find("Rico");
        }
        if (Rico is not null) {
            player_Status = Rico.GetComponent<IPlayer_Status>();
        }
    }
}
