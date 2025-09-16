using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    EnemyStatus enemyStatus;
    EnemyAttack enemyAttack;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite EnemyAtk, EnemyTired, EnemyDamage, EnemyDie;

    private Coroutine corTired;
    [SerializeField]
    private float tiredCooldown;
    private int turn = 0;
    private bool alive, running, damaged;
    public static event Action<bool> OnEnemyTired;
    private void OnEnable() {
        EnemyAttack.OnAtkFinished += Tired;
        EnemyStatus.OnEnemyHit += EnemyHit;
        EnemyStatus.OnEnemyDie += Die;
        BulletHell.UI.UIControl.OnStarted += Started;
    }
    private void OnDisable() {
        EnemyAttack.OnAtkFinished -= Tired;
        EnemyStatus.OnEnemyHit -= EnemyHit;
        EnemyStatus.OnEnemyDie -= Die;
        BulletHell.UI.UIControl.OnStarted -= Started;
    }
    private void Start() {
        enemyStatus = GetComponent<EnemyStatus>();
        enemyAttack = GetComponent<EnemyAttack>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        alive = true;
        running = false;
    }
    
    private void Started(bool value){
        running = value;
        Attacking();
    }
    private void Attacking(){
        OnEnemyTired?.Invoke(false);
        if(alive && running){
            spriteRenderer.sprite = EnemyAtk;
            turn++;
            enemyAttack.atkPre(turn);
        }
    }
    private void Die(){
        alive = false;
    }
    private void EnemyHit(){
        if (!damaged)
        {
            damaged = true;
            StopCoroutine(corTired);
            StartCoroutine(EnumEnemyHit());
        }
    }
    private IEnumerator EnumEnemyHit(){
        spriteRenderer.sprite = EnemyDamage;
        yield return new WaitForSeconds(0.5f);
        if (!alive){
            spriteRenderer.sprite = EnemyDie;
            OnEnemyTired?.Invoke(false);
        }
        Attacking();
    }
    private void Tired(){
        damaged = false;
        corTired = StartCoroutine(IETired());
    }
    private IEnumerator IETired(){
        spriteRenderer.sprite = EnemyTired;
        OnEnemyTired?.Invoke(true);
        yield return new WaitForSeconds(tiredCooldown);
        Attacking();
    }
}
