using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell.Enemy.Attacks
{
    public class Hands : AttacksSystem
    {
        [Header("Propriedades")]
        [SerializeField] private Transform[] presets;
        [SerializeField]
        private GameObject claw;
        [SerializeField] private float delay;
        [SerializeField]
        private GameObject player;
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            foreach (Transform preset in presets)
            {
                Debug.Log(preset.transform.position);
                Instantiate(claw, preset.transform.position, claw.transform.rotation);
                yield return new WaitForSeconds(delay);
            }
            
            Vector3 pos = new Vector3(player.transform.position.x, 3.5f , player.transform.position.z);
            Instantiate(claw, pos, claw.transform.rotation);
            //yield return new WaitForSeconds(delay);
            Debug.Log("Acabou");
            StartSelfDestruct(0f, this.gameObject);
        }
    }
}
