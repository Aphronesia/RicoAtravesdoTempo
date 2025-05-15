using UnityEngine;

namespace MenuMap{
    public class RicoMove : MonoBehaviour
    {
        [Header("Atributos do movimento")]
        private LevelManager _lvManager;
        [SerializeField]
        private LevelPopUp lvPop;
        public Rigidbody2D rig;
        private Vector3 _target;
        [SerializeField, Tooltip("velocidade do movimento")]
        private float speed;
        private bool _moving;

        public float distance;
        private void OnEnable() {
            LevelManager.OnStart += Started;
            LevelManager.OnTarget += Movement;
            LevelManager.OnTargetLoad += LoadPosition;
        }
        private void OnDisable() {
            LevelManager.OnStart -= Started;
            LevelManager.OnTarget -= Movement;
            LevelManager.OnTargetLoad -= LoadPosition;
        }
        private void Start() {
            GameObject levelController = GameObject.Find("LevelController");
            if (levelController != null){
                _lvManager = levelController.GetComponent<LevelManager>();
            }
            GameObject levelPop = GameObject.Find("Canvas/LevelMenu");
            if(levelPop !=null){
                lvPop = levelPop.GetComponent<LevelPopUp>();
            }
            rig = GetComponent<Rigidbody2D>();
        
            _moving = true;
        }
        public void Started(){
            Vector3 levelZeroPos = _lvManager.levels[0].objLevel.transform.position;
            transform.position = levelZeroPos;
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
            Vector3 currentPos = rig.position;
        

            Vector3 direction = _target - currentPos;
            distance = direction.magnitude;
            float step = speed * Time.fixedDeltaTime;
            if(distance <= step){
                rig.MovePosition(_target);
                rig.velocity = Vector3.zero;
            } else {
                Vector3 newPos = currentPos + (direction / distance) * step;
                rig.MovePosition(newPos);
            }
        }
        private void Movement(Vector3 newTarget){
            _target = newTarget;
        }

        private void LoadPosition(Vector3 target){
            transform.position = target;
        }
    }
}
