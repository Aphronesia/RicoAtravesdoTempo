using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;
    public static event Action OnAtkFinished;
    public List<Attack> attacks = new List<Attack>();
    public int atkLength;
    private int _lastAtk = -1;
    [System.Serializable]
    public class Attack{
        public GameObject atkPrefabs;
        public int repetitions;
        public float cooldown;
    }
    private void Awake() {
        atkLength = attacks.Count;
    }
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void atkPre(int index){
        if(index <= atkLength){
            //Debug.Log($"ataque predefinido index: "+ index + " atkLength: " + atkLength);
            StartCoroutine(AtkPredefined(index - 1));
        }
        if(index > atkLength){
            //Debug.Log($"ataque aleatorio index:"+ index + " atkLength: " + atkLength);
            AtkSelect();
        }
    }
    private IEnumerator AtkPredefined(int index){
        Attack attack = attacks[index];
        int reps = 0;
        while (reps < attack.repetitions){
            yield return new WaitForSeconds(2f);
            GameObject AtkInstan = Instantiate(attack.atkPrefabs, attack.atkPrefabs.transform.position, Quaternion.identity);
            reps++;
            yield return new WaitForSeconds(attack.cooldown);
        }
        reps = 0;
        OnAtkFinished?.Invoke();
    }
    private void AtkSelect()
    {
        int AtkIndex = UnityEngine.Random.Range(0, atkLength);
        
        // Previne repetição de ataque
        while (AtkIndex == _lastAtk)
        {
            AtkIndex = UnityEngine.Random.Range(0, atkLength);
        }
        _lastAtk = AtkIndex;
        Attack attackSelected = attacks[AtkIndex];
        StartCoroutine(AtkRandom(attackSelected.repetitions, attackSelected.cooldown, attackSelected.atkPrefabs));
    }
    private IEnumerator AtkRandom(int repetitions, float cooldown,GameObject AtkToSummon){
        int reps = 0;
        while (reps < repetitions){
            GameObject AtkInstan = Instantiate(AtkToSummon, AtkToSummon.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
            reps++;
        }
        OnAtkFinished?.Invoke();
    }
}
