using System;
using Autodesk.Fbx;
using UnityEngine;

namespace MinigameRitmo
{
    public class Score : MonoBehaviour
    {
        [SerializeField]private int points;
        [SerializeField]private int reward; // 10
        [SerializeField]private int combo;

        private void OnEnable()
        {
            ArrowCollider.OnClick += AddPoints;
            ArrowCollider.OnMiss += ResetCombo;
            Arrow.OnMiss += ResetCombo;
        }

        private void OnDisable()
        {
            ArrowCollider.OnClick -= AddPoints;
            ArrowCollider.OnMiss -= ResetCombo;
            Arrow.OnMiss -= ResetCombo;
            
        }

        public void AddPoints()
        {
            combo++;
            if (combo <= 3)
            {
                points += reward;
            }
            else if (combo >= 3 & combo <= 10)
            {
                points += reward * 2;
            }

        }

        public void ResetCombo()
        {
            Debug.Log("perdeu");
            combo = 0;
        }
        
    }
}
