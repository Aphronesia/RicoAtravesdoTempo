using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MinigameRitmo
{
    
    public class KeyboardControl : MonoBehaviour
    {
        [SerializeField] private ArrowCollider colliderUp;
        [SerializeField] private ArrowCollider colliderDown;
        [SerializeField] private ArrowCollider colliderLeft;
        [SerializeField] private ArrowCollider colliderRight;

        private void Start()
        {
            CheckVariavels();
            
                
        }

        private void CheckVariavels()
        {
            if (colliderUp is null)
            {
                Debug.LogWarning($"variavel {colliderUp.gameObject.name} nula");
                this.enabled = false;
            }

            if (colliderDown is null)
            {
                Debug.LogWarning($"variavel {colliderDown.gameObject.name} nula");
                this.enabled = false;
            }

            if (colliderLeft is null)
            {
                Debug.LogWarning($"variavel {colliderLeft.gameObject.name} nula");
                this.enabled = false;
            }

            if (colliderRight is null)
            {
                Debug.LogWarning($"variavel {colliderRight.gameObject.name} nula");    
                this.enabled = false;
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                colliderUp.Click();
            if (Input.GetKeyDown(KeyCode.DownArrow))
                colliderDown.Click();
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                colliderLeft.Click();
            if (Input.GetKeyDown(KeyCode.RightArrow))
                colliderRight.Click();

        }
    }
}
