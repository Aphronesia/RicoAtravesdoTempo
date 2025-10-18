using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool canDamage = false;
    [SerializeField]
    private int Damage;
    public static event Action<int> OnPlayerAtk;
    private void OnEnable() {
        EnemyControl.OnEnemyTired += EnemyTired;
    }
    private void OnDisable() {
        EnemyControl.OnEnemyTired -= EnemyTired;
    }
    private void EnemyTired(bool tired){
        canDamage = tired;
    }
    public void Attack(){
        if(canDamage){
            OnPlayerAtk?.Invoke(Damage);
        }

    }
}
