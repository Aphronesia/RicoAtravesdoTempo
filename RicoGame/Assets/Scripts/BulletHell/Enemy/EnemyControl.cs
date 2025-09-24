using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    EnemyStatus enemyStatus;
    EnemyAttack enemyAttack;
    SpriteRenderer spriteRenderer;
    Animator  animator;
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
        TakeComponents();
        alive = true;
        animator.SetBool("Alive", true);
        animator.SetBool("Tired", false);
        running = false;
    }
    
    private void Started(bool value){
        running = value;
        Attacking();
    }
    private void Attacking(){
        animator.SetBool("Tired", false);
        animator.SetTrigger("Attack");
        OnEnemyTired?.Invoke(false);
        if(alive && running){
            //.sprite = EnemyAtk;
            turn++;
            enemyAttack.atkPre(turn);
        }
    }
    private void Die(){
        animator.SetBool("Alive", false);
        animator.SetBool("Tired", true);
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
        //spriteRenderer.sprite = EnemyDamage;
        yield return new WaitForSeconds(0.5f);
        if (!alive){
            animator.SetBool("Alive", false);
            //spriteRenderer.sprite = EnemyDie;
            OnEnemyTired?.Invoke(false);
        }
        Attacking();
    }
    private void Tired(){
        damaged = false;
        corTired = StartCoroutine(IETired());
    }
    private IEnumerator IETired(){
        animator.SetBool("Tired", true);
        //spriteRenderer.sprite = EnemyTired;
        OnEnemyTired?.Invoke(true);
        yield return new WaitForSeconds(tiredCooldown);
        Attacking();
    }

    private void TakeComponents()
    {
        enemyStatus = GetComponent<EnemyStatus>();
        enemyAttack = GetComponent<EnemyAttack>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
}
