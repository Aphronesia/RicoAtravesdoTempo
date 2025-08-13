using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Camera mainCamera;
    private Animator anima;
    [SerializeField] private float delay;
    void Start()
    {
        //pega o objeto da camera
        mainCamera = Camera.main;
        anima = GetComponent<Animator>();
        if (mainCamera == null)
        {
            Debug.LogError($"Nenhuma camera encontrada");
            //desativa o script (não acontecera a animacao)
            enabled = false;
        }
        anima.SetBool("Break", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (VisibleOnCam())
        {
            StartCoroutine(Animation());
        }
        else
        {
            //Debug.Log($"nao ta visivel");
        }
    }

    IEnumerator Animation() {
        yield return new WaitForSeconds(delay);
        anima.SetBool("Break", true);
        //Debug.Log("Quebrou o chão");
        //desativa o script
        enabled = false;
    }
    bool VisibleOnCam()
    {
        Vector3 positionViewport = mainCamera.WorldToViewportPoint(transform.position);
        bool isVisible = positionViewport.x >= 0 && positionViewport.x <=1 && positionViewport.y >= 0 && positionViewport.y <= 1 && positionViewport.z > 0;
        //se tiver no campo de visao retorna true, se nao false
        return isVisible;

    }
}
