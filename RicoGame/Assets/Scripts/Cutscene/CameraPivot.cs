using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscene {
    public class CameraPivot : MonoBehaviour {

        public int cutsceneIndex;
        [SerializeField] private float durationMove;
        private Rigidbody rig;
        public List<Comic> comics = new List<Comic>();

        [Serializable]
        public class Comic {
            public List <Picture> pictures = new List<Picture>();
            [Serializable]
            public class Picture {
                public GameObject gObject;
                public float duration;
            }
        }

        private void Start() {
            TakeComponents();
            
        }

        public void GameStart(int index) {
            cutsceneIndex = index;
            Vector3 startPos = comics[index].pictures[index].gObject.transform.position;
            transform.position = startPos;
            StartCoroutine(Follows());
        }

        private IEnumerator Follows() {
            Debug.Log("Mover");
            foreach (Comic.Picture picture in comics[cutsceneIndex].pictures) {
                Vector3 origin = transform.position;
                float timee = 0f;
                while (timee < durationMove) {
                    timee += Time.fixedDeltaTime;
                    float t = timee / durationMove;
                    transform.position = Vector3.Lerp(origin, picture.gObject.transform.position, t);
                    yield return null;
                }

                Debug.Log("chegou no ="+ picture.gObject.name);
                yield return new WaitForSeconds(picture.duration);
            }
        }

        private void TakeComponents() {
            rig = GetComponent<Rigidbody>();
        }
    }

}
