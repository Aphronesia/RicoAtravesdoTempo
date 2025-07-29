using UnityEngine;

namespace MinigameTrem.PowerUps {
    public class Melon : MonoBehaviour, IPowerUps
    {
        private CapivaraTrem _capivaraTrem;
        // Start is called before the first frame update
        void Start() {
            TakeComponents();
        }

        public void Effect() {
            StartCoroutine(_capivaraTrem.InvisibilityTime());
            Destroy(gameObject);
        }

        private void TakeComponents() {
            GameObject rico = GameObject.Find("Rico");
            if (rico is not null) {
                _capivaraTrem = rico.GetComponent<CapivaraTrem>();
            }
        }
    }
}
