using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlUIMap : MonoBehaviour
{
    private ControlScenes controlScenes;
    private void Start() {
        ControlScenes ScenesController = FindObjectOfType<ControlScenes>();
        if (ScenesController != null){
            controlScenes = ScenesController.GetComponent<ControlScenes>();
        }
    }
}
