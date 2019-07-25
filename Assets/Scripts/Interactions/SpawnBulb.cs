using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnBulb : MonoBehaviour
{
    public static SpawnBulb Instance;

    public GameObject[] lightbulbs;
    public Transform spawnPosition;

    public Animator armAnimator;
    public GameObject spawnEffect;

    [HideInInspector]
    public GameObject bulb;

    private void Awake()
    {
        Instance = this;
    }

    public void BulbSpawn(ColorPalette color)
    {
        if (PutBulbOnSocket.Instance.socketedBulb != null)
        {
            PutBulbOnSocket.Instance.oldBulb = PutBulbOnSocket.Instance.socketedBulb;
            PutBulbOnSocket.Instance.socketedBulb = null;
            armAnimator.SetTrigger("RemoveBulb");
        }

        if (bulb != null)
            Destroy(bulb);

        Instantiate(spawnEffect, spawnPosition.position, spawnPosition.rotation);
        bulb = Instantiate(lightbulbs[Random.Range(0, lightbulbs.Length)], spawnPosition.position, Quaternion.identity);
        bulb.GetComponent<LightEffect>().lightColor = color;
    }
}
