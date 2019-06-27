using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnBulb : MonoBehaviour
{
    public GameObject[] lightbulbs;
    public Transform spawnPosition;

    private GameObject bulb;
    private bool doInfiniteSpawn;
    
    public void BulbSpawn(ColorPalette color)
    {
        if(bulb == null || doInfiniteSpawn)
        {
            bulb = Instantiate(lightbulbs[Random.Range(0, lightbulbs.Length)], spawnPosition.position, Quaternion.identity);
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
