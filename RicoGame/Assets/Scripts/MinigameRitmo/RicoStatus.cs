using System;
using UnityEngine;

namespace MinigameRitmo
{
    public class RicoStatus : MonoBehaviour
    {
        private Animator _animator;
        
        private void OnEnable()
        {
            RitmoControl.OnChange += ChangeAnim;
        }

        private void Start()
        {
            TakeComponents();
            
        }

        private void ChangeAnim()
        {
            _animator.SetTrigger("Change");
        }
        private void TakeComponents()
        {
            _animator = gameObject.GetComponent<Animator>();
        }
    }
}
