using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public Player_Status pStatus;
    List<HealthHeart> hearts = new List<HealthHeart>(); 
}
