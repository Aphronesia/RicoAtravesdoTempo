using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField]
    private Sprite atkBttnOn, atkBttnOff;
    [SerializeField]
    private Image AtkButton;
    private void OnEnable() {
        EnemyControl.OnEnemyTired += EnemyTired;
    }
    private void OnDisable() {
        EnemyControl.OnEnemyTired -= EnemyTired;
    }
    private void EnemyTired(bool value){
        if (value){
            AtkButton.sprite = atkBttnOn;
        }
        else{
            AtkButton.sprite = atkBttnOff;
        }
    }
}
