using System;
using System.Collections.Generic;
using UnityEngine;

namespace MinigameRitmo
{
    public class RitmoControl : MonoBehaviour
    {
        [SerializeField] private float timeRunning;
        private float _timeInitial;
        
        [Header("Setas")]
        
        [SerializeField] public GameObject up;
        [SerializeField] public GameObject down;
        [SerializeField] public GameObject left;
        [SerializeField] public GameObject right;
        
        [Header("Padrão")]
        public List<Arrow> arrows = new List<Arrow>();
        [System.Serializable]
        public class Arrow
        {
            public float time;
            public Direction direction01;
            public Direction direction02;
        }

        [SerializeField] private int lastIndex;
        [SerializeField] private Arrow nextArrow;
        private int _index = 0;
        public enum Direction
        {
            None,
            Up,
            Down,
            Left,
            Right,
            UpNo,
            DownNo,
            LeftNo,
            RightNo
        }
        private void Start()
        {
            _timeInitial  = Time.deltaTime;
        }

        private void FixedUpdate()
        {
            timeRunning = Time.fixedTime;
            SpawnArrows();
        }

        private void SpawnArrows()
        {
            if (timeRunning >= lastIndex + 1)
            {
                
            }
        }
    }
}
