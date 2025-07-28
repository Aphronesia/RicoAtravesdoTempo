using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] Obstacle;
    public GameObject[] Collectible;
    public GameObject[] PowerUp;
    int NumObstacle, NumCollectible, NumPower, SpawnValue;
    public bool PodeSpawn;
    public Transform posicaoRico;
    // Start is called before the first frame update
    void Start()
    {
        PodeSpawn = true;
        NumObstacle = Obstacle.Length;
        NumCollectible = Collectible.Length;
        NumPower = PowerUp.Length;
    }
    void Update()
    {
        Movimento();
    }
    void OnTriggerEnter2D(Collider2D colisionTrigger)
    {
        if (colisionTrigger.gameObject.CompareTag("SpawnObstacle")){
            //Debug.Log("colidiu com SpawnObstacle");
            Spawnar(0);
        }
        if (colisionTrigger.gameObject.CompareTag("SpawnPowerUp")){
            //Debug.Log("colidiu com SpawnPowerUp");
            Spawnar(1);
        }
        if (colisionTrigger.gameObject.CompareTag("SpawnCollectible")){
            //Debug.Log("colidiu com SpawnCollectible");
            Spawnar(2);
        }

        Debug.Log("Colidiu com" + colisionTrigger.name);
    }
    void Movimento()
    {
        Vector3 newposition = transform.position;
        newposition.x = posicaoRico.position.x + 10;
        transform.position = newposition;
    }
    void Spawnar(int TypeSpawn)
    {
        if (PodeSpawn){
            switch(TypeSpawn){
                case 0:
                //spawna obstaculo
                SpawnValue = Random.Range(0,NumObstacle);
                Instantiate(Obstacle[SpawnValue], transform.position, transform.rotation);
                break;
                case 1:
                SpawnValue = Random.Range(0,NumPower);
                Instantiate(PowerUp[SpawnValue], transform.position, transform.rotation);
                break;
                default:
                SpawnValue = Random.Range(0,NumCollectible);
                Instantiate(Collectible[SpawnValue], transform.position, transform.rotation);
                break;
            }
        }
    }
}
