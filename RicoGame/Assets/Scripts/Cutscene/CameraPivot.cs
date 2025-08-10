using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cutscene {
    public class CameraPivot : MonoBehaviour{

        public float testee;
        public int nextPictureIndex;
        public int comIndex;
        [SerializeField] private float durationMove;
        [SerializeField] private float durationFadeIn;
        [SerializeField] private float sizeCamMax;
        [SerializeField]
        private Camera cam;
        [SerializeField]
        private GameObject background;

        public Coroutine _runFollows = null;
        public List<Comic> comics = new List<Comic>();

        [Serializable]
        public class Comic{
            public string name;
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
            comIndex = index;
            foreach (Comic.Picture picture in comics[comIndex].pictures){
                SpriteRenderer sr = picture.gObject.GetComponent<SpriteRenderer>();
                Color cPic =  sr.color;
                sr.color = new Color(cPic.r, cPic.g, cPic.b, 0);
            }
            Vector3 startPos = comics[comIndex].pictures[0].gObject.transform.position;
            transform.position = startPos;
            _runFollows = StartCoroutine(Follows(index, 0));
        }

        public void SkipPicture(){
            StopCoroutine(_runFollows);
            _runFollows = null;
            _runFollows = StartCoroutine(Follows(comIndex, nextPictureIndex));
            
        }
        private IEnumerator Follows(int comicI, int startIndex) {
            // Caso esteja no ultimo quadro
            if (nextPictureIndex >= comics[comIndex].pictures.Count){
                SpriteRenderer previousPic =  comics[comIndex].pictures[comics[comIndex].pictures.Count - 1].gObject.GetComponent<SpriteRenderer>();
                Color cPreviousColor  = previousPic.color;
                previousPic.color = new Color(cPreviousColor.r, cPreviousColor.g, cPreviousColor.b, 1);
                Vector3 origin = transform.position;
                float time = 0f;
                float sizeStart = cam.orthographicSize;
                float diferenceSize = sizeCamMax - sizeStart;
                while (time < durationMove){
                    time += Time.fixedDeltaTime;
                    float t = time / durationMove;
                    float p = 2f;
                    t = 1f - Mathf.Pow(1f - t, p);
                    testee = t;
                    transform.position = Vector3.Lerp(origin, background.transform.position, t);
                    cam.orthographicSize = sizeStart + (diferenceSize * t);
                    yield return null;
                }
                _runFollows = null;
                yield break;
            }
            Vector3 startPos = comics[comicI].pictures[startIndex].gObject.transform.position;
            transform.position = startPos;
            // Debug.Log("Mover");
            // passa em quadrinho por quadrinho a partir da onde está
            for (int i = startIndex; i < comics[comIndex].pictures.Count; i++){
                if (i != 0){
                    SpriteRenderer previousPic =  comics[comIndex].pictures[i- 1].gObject.GetComponent<SpriteRenderer>();
                    Color cPreviousColor  = previousPic.color;
                    previousPic.color = new Color(cPreviousColor.r, cPreviousColor.g, cPreviousColor.b, 1);
                }
                nextPictureIndex = i + 1;
                Comic.Picture picture = comics[comIndex].pictures[i];
                Vector3 origin = transform.position;
                float time = 0f;
                while (time < durationMove) {
                    time += Time.fixedDeltaTime;
                    float t = time / durationMove;
                    float p = 2f;
                    t = 1f - Mathf.Pow(1f - t, p);
                    testee = t;
                    transform.position = Vector3.Lerp(origin, picture.gObject.transform.position, t);
                    yield return null;
                }

                time = 0f;
                SpriteRenderer srPicture = comics[comIndex].pictures[i].gObject.GetComponent<SpriteRenderer>();
                Color colorPic = srPicture.color;
                while (time < durationFadeIn){
                    time += Time.fixedDeltaTime;
                    float newAlpha  = Mathf.Clamp01(time / durationFadeIn);
                    srPicture.color = new Color(colorPic.r,  colorPic.g, colorPic.b, newAlpha);
                    yield return null;
                }
                // Debug.Log("chegou no ="+ picture.gObject.name);
                yield return new WaitForSeconds(picture.duration);
                
                // Depois do último quadrinho
                if (i == comics[comIndex].pictures.Count - 1){
                    nextPictureIndex = -1;
                    origin = transform.position;
                    time = 0f;
                    float sizeStart = cam.orthographicSize;
                    float diferenceSize = sizeCamMax - sizeStart;
                    while (time < durationMove){
                        time += Time.fixedDeltaTime;
                        float t = time / durationMove;
                        float p = 2f;
                        t = 1f - Mathf.Pow(1f - t, p);
                        testee = t;
                        transform.position = Vector3.Lerp(origin, background.transform.position, t);
                        cam.orthographicSize = sizeStart + (diferenceSize * t);
                        yield return null;
                    }
                    _runFollows = null;
                }
            }
        }

        private void TakeComponents() {
        }
    }

}
