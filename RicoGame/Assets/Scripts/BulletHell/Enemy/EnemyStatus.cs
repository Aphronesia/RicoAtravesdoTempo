using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField]
    private int health;
    public static event Action OnEnemyDie;
    public static event Action OnEnemyHit;
    private void OnEnable() {
        PlayerAttack.OnPlayerAtk += Damage;
    }
    private void OnDisable() {
        PlayerAttack.OnPlayerAtk -= Damage;
    }
    private void Damage(int value){
        if(health > 0){
            //Debug.Log($"perdi vida");
            health = health - value;
            OnEnemyHit?.Invoke();
        }
        if (health <= 0){
            //Debug.Log($"morri");
            health = 0;
            OnEnemyDie?.Invoke();
        }
    }
}
