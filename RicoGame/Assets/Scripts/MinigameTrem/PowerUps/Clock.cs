using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinigameTrem.PowerUps {
    public class Clock : MonoBehaviour, IPowerUps {
        [SerializeField, Range(0f, 1f)] private float timeScale;
        [SerializeField] private float cooldown;
        
        private SpriteRenderer _spriteRenderer;
        private void Start() {
            TakeComponents();
        }

        public void Effect() {
            StartCoroutine(IEEffect());

        }

        private IEnumerator IEEffect(){
            Time.timeScale = timeScale;
            _spriteRenderer.enabled = false;
            yield return new WaitForSeconds(cooldown);
            Time.timeScale = 1f;
            Destroy(gameObject);
        }

        private void TakeComponents() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
