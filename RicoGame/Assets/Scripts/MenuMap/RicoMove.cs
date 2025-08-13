using UnityEngine;

namespace MenuMap{
    public class RicoMove : MonoBehaviour
    {
        [Header("Atributos do movimento")]
        private LevelManager _lvManager;
        [SerializeField]
        private LevelPopUp lvPop;
        private Rigidbody2D _rig;
        private Animator _anima;
        private SpriteRenderer _spriteRen;
        private Vector3 _target;
        [SerializeField, Tooltip("velocidade do movimento")]
        private float speed;
        public bool moving;

        public float distance;
        private void OnEnable() {
            LevelManager.OnTarget += Movement;
        }
        private void OnDisable() {
            LevelManager.OnTarget -= Movement;
        }
        private void Start(){
            TakeComponents();
            
        
            moving = true;
        }
        private void Update() {
            if (moving && distance == 0){
                lvPop.LevelEnter(_lvManager.actualRico);
                moving = false;
                _anima.SetBool("walking", false);
                //Debug.Log("parou");
            }
            if (!moving && distance != 0){
                lvPop.LevelExit();
                moving = true;
                _anima.SetBool("walking", true);
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
            _spriteRen.flipX = direction.x > 0 ;
        }
        private void Movement(Vector3 newTarget, bool instantly){
            if (instantly){
                transform.position = newTarget;
                _target = newTarget;
            }
            if (!moving){
                _target = newTarget;
            }
        }

        private void TakeComponents(){
            GameObject levelController = GameObject.Find("LevelController");
            if (levelController != null){
                _lvManager = levelController.GetComponent<LevelManager>();
            }

            var levelPop = FindAnyObjectByType<LevelPopUp>();
            if(levelPop !=null){
                lvPop = levelPop.GetComponent<LevelPopUp>();
            }
            _rig = GetComponent<Rigidbody2D>();
            _anima = GetComponent<Animator>();
            _spriteRen = GetComponent<SpriteRenderer>();
        }
    }
}
