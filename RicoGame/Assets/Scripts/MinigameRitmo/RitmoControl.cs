using System;
using System.Collections.Generic;
using MinigameRitmo.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace MinigameRitmo
{
    public class RitmoControl : MonoBehaviour
    {
        [Header("Propriedades")]
        [SerializeField] private float timeRunning;
        [SerializeField] private int lastIndex;
        [SerializeField] private float lastTime;
        [SerializeField] private ArrowList nextArrow;
        private int _index = 0;

        private bool tocando;
        [Header("Setas")]
        [SerializeField] private List<ArrowObj> arrowProps = new List<ArrowObj>();
        
        [Header("Padrão")]
        public List<ArrowList> arrowsOrder = new List<ArrowList>();
        
        // Componentes 
        [SerializeField] private UIControl uiControl;
        // Eventos
        public static event Action OnChange;
        
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

        private void CheckMusician(bool value)
        {
            if (tocando != value)
            {
                tocando = value;
                Debug.Log("mudou");
                OnChange?.Invoke();
            }
        }
        private void ArrowSpawn()
        {
            // Ultima tecla 
            if (_index >= arrowsOrder.Count)
            {
                //Debug.Log("não espawna");
                return;
            }

            float time = timeRunning - lastTime;

            if (time >= nextArrow.time)
            {
                if (nextArrow.direction01 != Direction.None)
                {
                    Vector3 pos = arrowProps[(int)nextArrow.direction01].pos;
                    GameObject obj = arrowProps[(int)nextArrow.direction01].prefab;
                    GameObject arrowActual = Instantiate(obj, pos, Quaternion.identity);
                    if (arrowActual.GetComponent<Arrow>() != null)
                    {
                        arrowActual.GetComponent<Arrow>().ChangeMode(nextArrow.enemy);
                    }
                    //Debug.Log($"spawnou: {arrowsOrder[_index].direction01} index: {_index}");
                }

                if (nextArrow.direction02 != Direction.None) {
                    Vector3 pos = arrowProps[(int)nextArrow.direction02].pos;
                    GameObject obj = arrowProps[(int)nextArrow.direction02].prefab;
                    GameObject arrowActual = Instantiate(obj, pos, Quaternion.identity);
                    if (arrowActual.GetComponent<Arrow>() != null)
                    {
                        arrowActual.GetComponent<Arrow>().ChangeMode(nextArrow.enemy);
                    }
                    //Debug.Log($"spawnou: {arrowsOrder[_index].direction02} index: {_index}");
                }
                CheckMusician(nextArrow.enemy);
                lastIndex = _index;
                _index++;

                if (_index < arrowsOrder.Count) //
                {
                    nextArrow = arrowsOrder[_index];
                    lastTime = timeRunning;
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
