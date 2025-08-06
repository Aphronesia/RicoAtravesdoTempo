using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscene {
    public class CameraPivot : MonoBehaviour {

        public int cutsceneIndex;
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
        }

        public void GameStart(int index) {
            cutsceneIndex = index;
            Vector3 startPos = comics[index].pictures[index].gObject.transform.position;
            transform.position = startPos;
            StartCoroutine(Follows());
        }

        private IEnumerator Follows() {
            foreach (Comic.Picture picture in comics[cutsceneIndex].pictures) {
                yield return new WaitForSeconds(picture.duration);
            }
        }
    }

}
