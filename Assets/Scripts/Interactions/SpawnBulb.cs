using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnBulb : MonoBehaviour
{
    public GameObject[] lightbulbs;
    public Transform spawnPosition;

    private GameObject bulb;
    
    public void BulbSpawn(ColorPalette color)
    {
        if (bulb != null)
        {
            PutBulbOnSocket.Instance.ResetLights();
            Destroy(bulb.gameObject);
        }

        bulb = Instantiate(lightbulbs[Random.Range(0, lightbulbs.Length)], spawnPosition.position, Quaternion.identity);
        bulb.GetComponent<LightEffect>().lightColor = color;
    }
}
