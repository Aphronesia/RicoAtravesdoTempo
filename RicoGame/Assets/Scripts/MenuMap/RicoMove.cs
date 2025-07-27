using UnityEngine;

namespace MenuMap{
    public class RicoMove : MonoBehaviour
    {
        [Header("Atributos do movimento")]
        private LevelManager _lvManager;
        [SerializeField]
        private LevelPopUp lvPop;
        private Rigidbody2D _rig;
        private Vector3 _target;
        [SerializeField, Tooltip("velocidade do movimento")]
        private float speed;
        private bool _moving;

        public float distance;
        private void OnEnable() {
            LevelManager.OnTarget += Movement;
        }
        private void OnDisable() {
            LevelManager.OnTarget -= Movement;
        }
        private void Start(){
            TakeComponents();
            
        
            _moving = true;
        }
        private void Update() {
            if (_moving && distance == 0){
                lvPop.LevelEnter(_lvManager.actualRico);
                _moving = false;
                //Debug.Log("parou");
            }
            if (!_moving && distance != 0){
                lvPop.LevelExit();
                _moving = true;
                //Debug.Log("movendo");
            }
        }
        private void FixedUpdate() {
            //transform.position = Vector3.MoveTowards(transform.position, target, 0.5f);
            Vector3 currentPos = _rig.position;
        

            Vector3 direction = _target - currentPos;
            distance = direction.magnitude;
            float step = speed * Time.fixedDeltaTime;
            if(distance <= step){
                _rig.MovePosition(_target);
                _rig.velocity = Vector3.zero;
            } else {
                Vector3 newPos = currentPos + (direction / distance) * step;
                _rig.MovePosition(newPos);
            }
        }
        private void Movement(Vector3 newTarget, bool instantly){
            if (instantly){
                transform.position = newTarget;
            }
            _target = newTarget;
        }

        private void TakeComponents(){
            GameObject levelController = GameObject.Find("LevelController");
            if (levelController != null){
                _lvManager = levelController.GetComponent<LevelManager>();
            }
            GameObject levelPop = GameObject.Find("Canvas/LevelMenu");
            if(levelPop !=null){
                lvPop = levelPop.GetComponent<LevelPopUp>();
            }
            _rig = GetComponent<Rigidbody2D>();
        }
    }
}
