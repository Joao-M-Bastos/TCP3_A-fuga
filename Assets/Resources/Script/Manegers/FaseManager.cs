using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseManager : MonoBehaviour
{
    PlayerSpawnerScrp playerSpawnerScrp;

    private void OnLevelWasLoaded(int level)
    {
        playerSpawnerScrp = GameObject.FindGameObjectWithTag("RespawnManegar").GetComponent<PlayerSpawnerScrp>();
        playerSpawnerScrp.SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
