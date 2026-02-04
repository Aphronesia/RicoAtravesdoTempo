using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Camera))]
    public class CameraSize : MonoBehaviour
    {
        public float referenceAspect = 16f / 9f;

        private float originalOrthoSize;
        private Vector3 originalPosition;

        void Awake()
        {
            Camera cam = GetComponent<Camera>();
            originalOrthoSize = cam.orthographicSize;
            originalPosition = cam.transform.position;
        }

        void Start()
        {
            Camera cam = GetComponent<Camera>();
            float screenAspect = (float)Screen.width / Screen.height;

            // Se não for tela larga, mantém tudo igual
            if (screenAspect <= referenceAspect)
            {
                cam.orthographicSize = originalOrthoSize;
                cam.transform.position = originalPosition;
                return;
            }

            // Aplica zoom
            float scale = referenceAspect / screenAspect;
            float newSize = originalOrthoSize * scale;
            cam.orthographicSize = newSize;

            // Diferença de altura (em unidades do mundo)
            float heightDiff = originalOrthoSize - newSize;

            // Desce a câmera para compensar o corte inferior
            cam.transform.position = originalPosition + Vector3.down * heightDiff;
        }
    }
}
