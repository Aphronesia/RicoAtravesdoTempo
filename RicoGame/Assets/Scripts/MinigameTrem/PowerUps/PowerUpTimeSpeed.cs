using System;
using System.Collections;
using System.Collections.Generic;
using MinigameTrem.Scoreboard;
using UnityEngine;
using TMPro;

namespace MinigameTrem.PowerUps
{
    public class PowerUpTimeSpeed : MonoBehaviour, IPowerUps
    {
        [SerializeField] private float cooldown;
        
        public TMP_Text timeText;
        private ScoreManager _scoreManager;
        private SpriteRenderer _spriteRenderer;
        private void Start(){
            TakeComponents();
        }
        public void Effect()
        {
            StartCoroutine(IEEffect());
        }

        private IEnumerator IEEffect()
        {
            _scoreManager.cooldown = 0.05f;
            timeText.color = Color.yellow;
            _spriteRenderer.enabled = false;
            yield return new WaitForSeconds(cooldown);
            _scoreManager.cooldown = 0.1f;
            timeText.color = Color.white;
            Destroy(gameObject);
        }
        private void TakeComponents(){
            _spriteRenderer = GetComponent<SpriteRenderer>();
            GameObject score = GameObject.Find("ScoreController");
            if (score != null) {
                _scoreManager = score.GetComponent<ScoreManager>();
                timeText = _scoreManager.timeText;
            }
        }
    }
}
