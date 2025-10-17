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
            RitmoControl.OnEndMusic += EndAnim;
        }

        private void OnDisable()
        {
            RitmoControl.OnChange -= ChangeAnim;
            RitmoControl.OnEndMusic -= EndAnim;
        }

        private void Start()
        {
            TakeComponents();
        }

        private void ChangeAnim()
        {
            _animator.SetTrigger("Change");
        }

        private void EndAnim()
        {
            _animator.SetTrigger("End");
        }
        private void TakeComponents()
        {
            _animator = gameObject.GetComponent<Animator>();
        }
    }
}
