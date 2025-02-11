using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ferle : MonoBehaviour
{
    public bool alive, horny, dating, programming;
    void Start ()
    {
        alive = true;
    }
    void Update(){
        //não questione pfvr
        if (alive){
            horny = true;
        }
        Girlfriend girlfriend = FindObjectOfType<Girlfriend>();
        if (girlfriend != null){
            dating = true;
        }
        else{
            dating = false;
        }
        Debug.Log($"ferle is " + CurrentEmotionalState());
    }
    private string CurrentEmotionalState(){
        var emotionalNumber = (horny, dating, programming);
        switch (emotionalNumber){
            case (false, false, false):
                return "procrastinating";
            case (true, false, false):
                return "sad";
            case (false, true, false):
                return "happy (but this is strange)";
            case (false, false, true):
                return "nerd";
            case (true, true, false):
                return "very happy";
            case (false, true, true):
                return "happy";
            case (true, false, true):
                return "happy? (this doesn't even make sense)";
            default:
                return "listening music";
        }
    }
}
