using System;
using UnityEngine;

namespace MinigameTrem{
    public class chaoTrem : MonoBehaviour
    {
        public Transform posicaoRico;
        private bool _canFollow;

        private void OnEnable(){
            Scoreboard.ScoreManager.OnGanhou += Ganhou;
        }
        private void OnDisable(){
            Scoreboard.ScoreManager.OnGanhou -= Ganhou;
        }

        private void Ganhou(){
            _canFollow = false;
        }

        private void Start(){
            _canFollow = true;
        }

        void Update()
        {
            Movimento();
        }
        void Movimento()
        {
            if (_canFollow){
            Vector3 novaPosicao = transform.position;
            novaPosicao.x = posicaoRico.position.x;
            transform.position = novaPosicao;
            }

        }
    }
}
