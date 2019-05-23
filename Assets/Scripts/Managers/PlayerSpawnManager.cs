using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPosition;

    private void Start()
    {
        Instantiate(playerPrefab, spawnPosition.position, Quaternion.identity);
    }
}
