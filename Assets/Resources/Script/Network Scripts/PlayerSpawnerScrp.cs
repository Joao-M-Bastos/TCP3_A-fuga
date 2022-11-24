using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawnerScrp : MonoBehaviour
{
    public GameObject[] preFabs;
    public GameObject[] botPreFabs;
    public Transform[] spawnPoints;

    void Start()
    {
        int random = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[random];
        GameObject playerToSpawn = preFabs[0];//(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
        int playersNumber = PhotonNetwork.CurrentRoom.PlayerCount;

        while (playersNumber < 6)
        {
            int botRandom = Random.Range(0, spawnPoints.Length);
            Transform botSpawnPoint = spawnPoints[botRandom];
            GameObject botToSpawn = botPreFabs[(int)Mathf.Floor(Random.Range(0, 3))];
            Instantiate(botToSpawn, botSpawnPoint.position, Quaternion.identity);
            playersNumber = playersNumber + Random.Range(1, 1);
        }
    }
}
