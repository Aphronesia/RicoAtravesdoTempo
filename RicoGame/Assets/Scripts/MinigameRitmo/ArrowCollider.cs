using System;
using UnityEngine;

namespace MinigameRitmo
{
    public class ArrowCollider : MonoBehaviour
    {
        [SerializeField]
        private GameObject _arrow;
        
        public static event Action OnClick;
        public static event Action OnMiss;
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
            if (_arrow == null)
            {
                OnMiss?.Invoke();
                return;
            }
            OnClick?.Invoke();
            Destroy(_arrow);
        }
        
    }
}
