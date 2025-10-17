using System;
using Autodesk.Fbx;
using MinigameRitmo.UI;
using UnityEngine;

namespace MinigameRitmo
{
    public class Score : MonoBehaviour
    {
        [SerializeField]private int points;
        [SerializeField] private int pointsToWin;
        [SerializeField]private int reward; // 10
        [SerializeField]private int combo;
        
        [SerializeField]
        private UIControl uiControl;
        
        private void OnEnable()
        {
            ArrowCollider.OnClick += AddPoints;
            ArrowCollider.OnMiss += ResetCombo;
            ArrowCollider.OnEnemy += EnemyPoints;
            Arrow.OnMiss += ResetCombo;
        }

        private void OnDisable()
        {
            ArrowCollider.OnClick -= AddPoints;
            ArrowCollider.OnMiss -= ResetCombo;
            ArrowCollider.OnEnemy += EnemyPoints;
            Arrow.OnMiss -= ResetCombo;
            
        }
        private void Start()
        {
            TakeComponents();
        }

        public bool GetScore()
        {
            if (points >= pointsToWin)
                return true;
            return false;
        }
        public void AddPoints()
        {
            combo++;
            if (combo <= 3)
            {
                points += reward;
            }
            else if (combo >= 3)
            {
                points += reward * 2;
            }
            uiControl.RefreshTextPontos(points);
            uiControl.RefreshTextCombos(combo);

        }

        public void ResetCombo()
        {
            //Debug.Log("perdeu");
            combo = 0;
            uiControl.RefreshTextCombos(combo);
        }

        public void EnemyPoints()
        {
            if (points >= 10)
            {
                points -= 10;
            }
            else if (points < 10)
            {
                points = 0;
            }
            ResetCombo();
        }
        private void TakeComponents()
        {
            uiControl = GameObject.FindObjectOfType<UIControl>().GetComponent<UIControl>();
        }
    }
}
