using System;
using System.Collections;
using UnityEngine;

namespace BulletHell.Player {
    public class Player_Status : MonoBehaviour, Game.Player.IPlayer_Status
    {
        [Header("Atribudos do Rico")]
        [Tooltip("valor inteiro da vida")]
        public int health;
        public int healthMax;

        public int Ihealth {
            get => health;
            set => health = value;
        }

        public int IhealthMax {
            get => healthMax; 
            set => healthMax = value;
        }
        [SerializeField, Tooltip("tempo em segundos de invulnerabilidade")]
        private float invulnerable;
        private float cooldown, timer;
        [SerializeField, Tooltip("transparencia do blink da invulnerabelidade")]
        private Color color;
        private SpriteRenderer spriteRenderer;
        public static event Action OnPlayerDamaged;
        public static event Action OnPlayeDeath;
        
        private void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            color = spriteRenderer.color;
 
            // descomentar isso dps
            //health = 6;
            //healthMax = health;
        }
        private void Update() {
            timer += Time.deltaTime;
        }
        private void OnTriggerEnter2D(Collider2D other) {
            switch (other.gameObject.tag)
            {
                case "Monstros":
                    Damage(1);
                    break;
                default:
                    break;
            }
        }
        private void Damage(int value){
            if (timer > cooldown){
                StartCoroutine(VisualInvunerable());
                health = health - value;
                cooldown = timer + invulnerable;
                OnPlayerDamaged?.Invoke();
            }
            if (health <= 0){
                OnPlayeDeath?.Invoke();
            }
        }
        private IEnumerator VisualInvunerable(){
            do{
                yield return new WaitForSeconds(0.05f);
                color.a = 0f;
                spriteRenderer.color = color;
                yield return new WaitForSeconds(0.05f);
                color.a = 1f;
                spriteRenderer.color = color;
            }while (timer < cooldown);
        }
    }
}
