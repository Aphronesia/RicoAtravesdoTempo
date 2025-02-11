using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Destruction());
    }
    private IEnumerator Destruction()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
