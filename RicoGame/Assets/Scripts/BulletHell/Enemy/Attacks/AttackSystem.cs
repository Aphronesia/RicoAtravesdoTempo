using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksSystem : MonoBehaviour
{
    public EnemyAttack enemyAttack;
    /// <summary>
    /// Inicia a coroutine de autodestruição com o delay especificado.
    /// </summary>
    /// <param name="delay">Tempo de espera (em segundos) antes de destruir o objeto.</param>
    protected void StartSelfDestruct(float delay, GameObject destroyer){
        StartCoroutine(DestroyAfterDelay(delay, destroyer));
    }
    /// <summary>
    /// Coroutine que espera pelo tempo especificado e depois destrói o objeto.
    /// </summary>
    /// <param name="delay">Tempo de espera (em segundos) antes da destruição.</param>
    protected virtual IEnumerator DestroyAfterDelay(float destructionDelay, GameObject destroyer){
        yield return new WaitForSeconds(destructionDelay);
        Destroy(destroyer);
    }
}
