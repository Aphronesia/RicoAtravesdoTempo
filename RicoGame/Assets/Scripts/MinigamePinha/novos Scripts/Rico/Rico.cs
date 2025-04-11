using System;
using UnityEngine;


public class Rico : MonoBehaviour
{
    Rigidbody2D rig;
    public SpriteRenderer spriteRenderer;
    [SerializeField] private Vector3[] targetPositions; //array das cordenadas 
    [SerializeField]  private float speed = 5f, impulse; 
    private int currentTargetIndex = 0;
    private bool isMoving = false, turn = true; 
    private Animator anima;
    public Sprite RicoDead;
    public int heart;  
    [SerializeField] private int maxHealth = 3;
    public bool ricoDied = false;

    void Start()
    {
        ricoDied = false;
        rig = GetComponent<Rigidbody2D>();
        heart = maxHealth;
        anima = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
         if (spriteRenderer == null) {
             Debug.LogError("SpriteRenderer não encontrado no objeto.");
        } 

    }

    void Update()
    {
        if (heart <= 0 && !ricoDied){
             Died();  
        }
        if (ricoDied) return; // Se morreu, não faz mais nada
        
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            if (!isMoving && currentTargetIndex < targetPositions.Length - 1){
                isMoving = true; 
                Flip(false);
                currentTargetIndex++; // Avança para a próxima coordenada
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            if (!isMoving && currentTargetIndex >= 0){
                isMoving = true; 
                 Flip(true);
                currentTargetIndex--; // Retrocede para a coordenada anterior
            }
        }

        if (isMoving){
            MoverRico();
            anima.SetBool("CanRoll",true);
        }
        else{
            anima.SetBool("CanRoll",false);
        }
    }
    void MoverRico()
    {
        // Move o rico direção à coordenada atual
        transform.position = Vector3.MoveTowards(transform.position, targetPositions[currentTargetIndex], speed * Time.deltaTime);

        // Verifica se chegou na coordenada
        if (transform.position == targetPositions[currentTargetIndex]){
            isMoving = false; 
        }
    }
    void Flip(bool flipToRight)
    {
        if ((flipToRight && !turn) || (!flipToRight && turn)){
            turn = !turn;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
     public void Died(){
        if (ricoDied) return;

        ricoDied = true;
        spriteRenderer.sprite = RicoDead;
        isMoving = false;
        turn = false;
        
        if (rig != null){
            rig.bodyType=RigidbodyType2D.Dynamic;
            rig.AddForce(Vector2.up * impulse,ForceMode2D.Impulse);
        }

         if (anima != null) {
        anima.enabled = false;
    }
    
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

      ControleUI.Instance.ShowDiedPanelComDelay();
    }


    public void Damage()
    {
          if (ricoDied) return; // Se já morreu, não toma mais dano
        heart = Mathf.Max(0, heart - 1); // Garante que a vida não fique negativa
        if (heart <= 0){
            Died();
        }
    }
     public void Heal(int healAmount)
    {
        if (ricoDied) return; // Se está morto, não pode ser curado
        heart = Mathf.Min(heart + healAmount, maxHealth); // Cura sem ultrapassar o máximo
        Debug.Log("Vida atual: " + heart);
    }
      
}