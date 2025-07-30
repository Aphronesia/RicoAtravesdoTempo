using System.Collections;
using UnityEngine;

namespace MinigameTrem{
    public class CapivaraTrem : MonoBehaviour
    {
        public bool canWalk, canJump, chao, Vivo, invisibility, hyperJump;
        private float veloc = 6.0f, salto = 8.0f, impulso = 8f;
        private Animator anima;
        public Rigidbody2D rig;
        Vector3 PosicaoInicial;
        [SerializeField]
        private float FinalTrem;
        [SerializeField]
        private Monstros monstros;
        [SerializeField]
        private ControleUITrem controleui;
        public BoxCollider2D playerCollider;
        private SpriteRenderer spriteRenderer;
        public Scoreboard.ScoreManager scoreManager;
        private Color color;
    
        public SoundControllerTrem soundTrem;

        private void OnEnable(){
            Scoreboard.ScoreManager.OnGanhou += Ganhou;
        }

        private void OnDisable(){
            Scoreboard.ScoreManager.OnGanhou -= Ganhou;
        }

        private void Ganhou(){
            invisibility = true;
        }
        private void Start(){
            TakeComponents();
            Vivo = true;
            canWalk = false;
            canJump = false;

            anima.SetBool("Walk", false);
            anima.SetBool("Dead", false);
            color = spriteRenderer.color;

        }
        private void Update()
        {
            Teleporte();
        }
        void FixedUpdate()
        {
            Movimento();
        }
        //esse void inicia o jogo pra capivara de fato, ele é puxado la no controleui
        public void StartGame()
        {
            canWalk = true;
            canJump = true;
            anima.SetBool("Walk", true);
            anima.SetBool("Dead", false);
        }
        private void Movimento()
        {
            if (canWalk)
            {
                rig.velocity = new Vector2(veloc, rig.velocity.y);
            }
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            //Debug.Log("colidiu com " + other.gameObject.tag);
            if (other.gameObject.CompareTag("Chao") && Vivo)
            {
                chao = true;
            }
            if (other.gameObject.CompareTag("PowerUp1"))
            {
                PowerUps.IPowerUps powerUps = other.gameObject.GetComponent<PowerUps.IPowerUps>();
                if (powerUps != null) {
                    powerUps.Effect();
                }
                
            }
            if (invisibility)
            {
                if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("TopObstacle"))
                {
                    Debug.Log("ignorou colisao");
                    BoxCollider2D collider = other.collider as BoxCollider2D;
                    Physics2D.IgnoreCollision(playerCollider, collider);
                }
            }
            else if(!invisibility)
            {
                if (other.gameObject.CompareTag("TopObstacle"))
                {
                    ContactPoint2D[] contacts = other.contacts;
                    Vector2 normal = contacts[0].normal;
                    if (normal.y > 0.5f){
                        chao = true;
                    }
                    else if (Mathf.Abs(normal.x) > 0.5f){
                        Morreu();
                    }
                }
                if (other.gameObject.CompareTag("Obstacle")) {
                    Morreu();
                }
                //if (other.gameObject.CompareTag("TopObstacle"))
                //{
                //   chao = true;
                //}
            }
        }
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Spring") && !invisibility)
            {
                hyperJump = true;
            }
        }
        private void Morreu()
        {
            if (Vivo)
            {
                controleui.Morreu();
                monstros.RicoDead();
                Vivo = false;
                canWalk = false;
                anima.SetBool("Walk", false);
                anima.SetBool("Dead", true);
                rig.AddForce(Vector2.up * 0, ForceMode2D.Impulse);
                rig.AddForce(Vector2.up * impulso, ForceMode2D.Impulse);
            }
        }
        public void Jump()
        {
            soundTrem.PlaySound(0);
            if (canJump && chao && Vivo && !hyperJump)//salto normal
            {
                rig.AddForce(Vector2.up * 0, ForceMode2D.Impulse);
                rig.AddForce(Vector2.up * salto, ForceMode2D.Impulse);
                chao = false;
            }
            else if (canJump && chao && Vivo && hyperJump)//super salto (em contato com a caixa de som)
            {
                rig.AddForce(Vector2.up * 0, ForceMode2D.Impulse);
                rig.AddForce(Vector2.up * (salto + 5.0f), ForceMode2D.Impulse);
                chao = false;
                hyperJump = false;
            }

        }
        //teleporta o rico de volta quando chega no fim do trem, pra dar a ilusao de que o trem e infinito
        public void Teleporte()
        {
            if (transform.position.x > FinalTrem)
            {
                PosicaoInicial = new Vector3(0, transform.position.y, 0);
                transform.position = PosicaoInicial;
            }
        }

        public void TriggerInvisibility(){
            StartCoroutine(InvisibilityTime());
        }
        private IEnumerator InvisibilityTime()
        {
            invisibility = true;
            color.a = 0.5f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(3);
            invisibility = false;
            color.a = 1f;
            spriteRenderer.color = color;
        }

        private void TakeComponents(){
            rig = GetComponent<Rigidbody2D>();
            anima = GetComponent<Animator>();
            playerCollider = GetComponent<BoxCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        
        }
    }
}