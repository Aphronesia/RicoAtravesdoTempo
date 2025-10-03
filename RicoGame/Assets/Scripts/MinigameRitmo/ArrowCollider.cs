using System;
using MinigameRitmo.UI;
using UnityEngine;

namespace MinigameRitmo
{
    public class ArrowCollider : MonoBehaviour
    {
        [SerializeField]
        private GameObject _arrow;
        
        public static event Action OnClick;
        public static event Action OnMiss;
        public static event Action OnEnemy;

        

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "Pinha":
                    _arrow = other.gameObject;
                    break;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "Pinha":
                    _arrow = null;
                    break;
            }
        }

        public void Click()
        {
            if (_arrow is null)
            {
                OnMiss?.Invoke();
                Debug.Log("errou");
                return;
            }
            if (_arrow.GetComponent<Arrow>() is null)
                return;
            if (_arrow.GetComponent<Arrow>() is  null)
                return;
            
            switch (_arrow.GetComponent<Arrow>().enemy)
            {
                case true:
                    OnEnemy?.Invoke();
                    Debug.Log("do inimigo");
                    break;
                case false:
                    OnClick?.Invoke();
                    Debug.Log("acertou");
                    
                    break;
            }
            
        }

        
    }
}
