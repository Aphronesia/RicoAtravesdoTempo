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
        private GameObject player, enemy;

        [SerializeField] private int repts;
        public List<Target> targets= new List<Target>();
        [Serializable]
        public class Target
        {
            public GameObject obj;
            public int order;
        }
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            enemy = GameObject.Find("Enemy");
            StartCoroutine(Spawn());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator Spawn()
        {
            int orderMax = 0;
            if (targets.Count != 0)
            {
                foreach (Target t in targets)
                {
                    if (t.order >= orderMax)
                    {
                        orderMax = t.order;
                    }
                }
                int objActual = 0;
                while (objActual <= orderMax)
                {
                    foreach (Target t in targets)
                    {
                        if (t.order == objActual)
                            Instantiate(claw, t.obj.transform.position, claw.transform.rotation);
                        Animator enemyAnim = enemy.GetComponent<Animator>();
                        enemyAnim.SetTrigger("Attack");
                    }

                    if (orderMax != 0)
                    {
                        yield return new WaitForSeconds(delay);
                        objActual++;
                    }
                }
            }
            /*
            foreach (Transform preset in presets)
            {
                //Debug.Log(preset.transform.position);
                Instantiate(claw, preset.transform.position, claw.transform.rotation);
                yield return new WaitForSeconds(delay);
            }
            */
            
            int rps = 0;
            while (rps <= repts && repts > 0)
            {
                Vector3 pos = new Vector3(player.transform.position.x, 3.5f , player.transform.position.z);
                Instantiate(claw, pos, claw.transform.rotation);
                yield return new WaitForSeconds(delay);
                rps++;
                Animator enemyAnim = enemy.GetComponent<Animator>();
                enemyAnim.SetTrigger("Attack");
            }
            Debug.Log("Acabou");
            StartSelfDestruct(0f, this.gameObject);
        }
    }
}
