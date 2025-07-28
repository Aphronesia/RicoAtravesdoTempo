using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

namespace MinigameTrem.Cenario {
    public class CeilingLightTrem : MonoBehaviour{
        [SerializeField] private bool blink;
        private float _randomCooldown;
        [SerializeField]
        private Light2D _light;

        private void Start(){
            TakeComponents();
            blink = true;
            StartCoroutine(BlinkLight());
        }

        IEnumerator BlinkLight(){
            while (blink){
                _light.enabled = true;
                _randomCooldown = UnityEngine.Random.Range(2f,7f);
                yield return new WaitForSeconds(_randomCooldown);
                _light.enabled = false;
                yield return new WaitForSeconds(0.2f);
            
                if (Random.value > 0.5f){
                    _light.enabled = true;
                    yield return new WaitForSeconds(0.2f);
                    _light.enabled = false;
                    yield return new WaitForSeconds(0.2f);
                }
            }
       
        }
        private void TakeComponents(){
            _light = GetComponent<Light2D>();
        }
    }
}
