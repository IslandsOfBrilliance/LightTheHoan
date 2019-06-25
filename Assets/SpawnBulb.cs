using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnBulb : MonoBehaviour{
    private GameObject bulb;
    public Transform spawnPosition;
    public GameObject lightbulb;
    public void BulbSpawn(){
        if(bulb == null){
            bulb = Instantiate(lightbulb, spawnPosition.position, Quaternion.identity);
        }
    }
}
