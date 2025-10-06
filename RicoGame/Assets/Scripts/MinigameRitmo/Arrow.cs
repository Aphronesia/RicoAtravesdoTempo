using System;
using UnityEngine;

namespace MinigameRitmo
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private float speedDown;
        private Rigidbody2D rb;
        public bool enemy;

        [SerializeField] private Color colorEnemy;
        private Color colorWhite = Color.white;

        private SpriteRenderer _spriteRenderer;
        public static event Action OnMiss;
        private void Start()
        {
            TakeComponent();
        }

        private void FixedUpdate()
        {
            rb.velocity = new Vector2(rb.velocity.x, -speedDown);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "Bigorna": // Fundo
                    Destroy(gameObject);
                    if(!enemy)
                        OnMiss?.Invoke();
                    break;
                
            
            }
        }

        public void ChangeMode(bool value)
        {
            enemy = value;
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if(_spriteRenderer == null)
                return;
            if (enemy)
            {
                _spriteRenderer.color = colorEnemy;
            }
            else
            {
                _spriteRenderer.color = colorWhite;
            }
        }
        private void TakeComponent()
        {
            rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
    }
}
