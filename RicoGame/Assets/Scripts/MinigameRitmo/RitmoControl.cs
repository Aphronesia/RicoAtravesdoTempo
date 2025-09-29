using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MinigameRitmo
{
    public class RitmoControl : MonoBehaviour
    {
        [SerializeField] private float timeRunning;
        [SerializeField] private int lastIndex;
        [SerializeField] private float lastTime = 0;
        [SerializeField] private ArrowList nextArrow;
        private int _index = 0;
        [Header("Setas")]
        [SerializeField] private List<ArrowObj> arrowProps = new List<ArrowObj>();
        [Header("Padrão")]
        public List<ArrowList> arrowsOrder = new List<ArrowList>();

        private void Start()
        {
            nextArrow = arrowsOrder[0];
        }

        private void FixedUpdate()
        {
            timeRunning = Time.fixedTime;
            if (arrowsOrder.Count == 0)
            {
                return;
            }
            
            ArrowSpawn();
        }
        
        private void ArrowSpawn()
        {
            if (_index >= arrowsOrder.Count)
            {
                Debug.Log("não espawna");
                return;
            }

            float time = timeRunning - lastTime;

            if (time >= nextArrow.time)
            {
                if (nextArrow.direction01 != Direction.None)
                {
                    Vector3 pos = arrowProps[(int)nextArrow.direction01].pos;
                    GameObject obj = arrowProps[(int)nextArrow.direction01].prefab;
                    GameObject arrow = Instantiate(obj, pos, Quaternion.identity);
                    Debug.Log($"spawnou: {arrowsOrder[_index].direction01} index: {_index}");
                }

                //if (nextArrow.direction02 != Direction.None) {
                //    // spawna outra flecha
                //}

                lastIndex = _index;
                _index++;

                if (_index < arrowsOrder.Count) // <- protege contra acesso fora da lista
                {
                    nextArrow = arrowsOrder[_index];
                    lastTime = timeRunning;
                }
                else
                {
                    Debug.Log("acabaram as flechas");
                }
            }
        }
        

    }
    [System.Serializable]
    public class ArrowObj
    {
        public GameObject prefab;
        public Vector3 pos;
    }
    [System.Serializable]
    public class ArrowList
    {
        public float time;
        public bool enemy;
        public Direction direction01;
        public Direction direction02;
    }

    public enum Direction
    {
        None, // 0
        Up, // 1
        Down, // 2
        Left, // 3
        Right, // 4
    }
}
