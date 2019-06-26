using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnBulb : MonoBehaviour
{
    private GameObject bulb;
    public Transform spawnPosition;
    public GameObject lightbulb;

    private bool doInfiniteSpawn;
    
    public void BulbSpawn(ColorPalette color)
    {
        if(bulb == null || doInfiniteSpawn)
        {
            bulb = Instantiate(lightbulb, spawnPosition.position, Quaternion.identity);
            bulb.GetComponent<LightEffect>().lightColor = color;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            doInfiniteSpawn = !doInfiniteSpawn;
        }
    }
}
