using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell.Enemy.Attacks
{
    public class ClawScratch : AttacksSystem
    {
        [Header("configuração AutoDestruição")]
        [SerializeField] private float delayFallClaw;
        [SerializeField] private float delayClawDestruct;
        [SerializeField] private float delaySelfDestruct;
        [SerializeField] private float delayFadeScratch;
        [SerializeField] private float gravityScale;
        
        [Header("Componentes")]
        [SerializeField] private Transform claw;
        [SerializeField] private Rigidbody2D clawRb;
        private SpriteRenderer _scratchSprite;

        private float _dist;
        private float _scratchStartY, _scratchStartSizeY;
        
        private void Start()
        {
            TakeComponents();
            _scratchStartY = claw.position.y;
            _scratchStartSizeY = _scratchSprite.size.y;
            StartCoroutine(Fade());
            StartSelfDestruct(delayClawDestruct + delayFallClaw, claw.gameObject);
        }

        private IEnumerator Fade()
        {
            yield return new WaitForSeconds(delayFallClaw);
            clawRb.gravityScale = gravityScale;
            yield return new WaitForSeconds(delayFallClaw + delaySelfDestruct);
            var startA = _scratchSprite.color.a;
            var color = _scratchSprite.color;
            float time = 0f;
            while (time < delayFadeScratch)
            {
                time += Time.deltaTime;
                color.a = Mathf.Lerp(startA, 0f, time / delayFadeScratch);
                _scratchSprite.color = color;
                yield return null;
            }
            color.a = 0f;
            _scratchSprite.color = color;
            StartSelfDestruct(0f, gameObject);

        }
        private void FixedUpdate()
        {
            if (!claw) return;
            _dist = _scratchStartY - claw.position.y;
            MoveScratch();
            ExtendedScratch();
            
        }

        private void MoveScratch()
        {
            float center = _scratchStartY - (_dist * 0.5f);
            var scratchNewPosition = new Vector3(transform.position.x, center, transform.position.z);
            transform.position = scratchNewPosition;
        }

        private void ExtendedScratch()
        {
            float diference = _scratchStartSizeY + _dist;
            var scratchNewSpriteSize = new Vector2(_scratchSprite.size.x, diference);
            _scratchSprite.size = scratchNewSpriteSize;
        }

        private void TakeComponents()
        {
            _scratchSprite = GetComponent<SpriteRenderer>();
        }
    }
}
