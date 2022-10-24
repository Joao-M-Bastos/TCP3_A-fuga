using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawnerScrp : MonoBehaviour
{
    public GameObject[] preFabs;
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[random];
        GameObject playerToSpawn = preFabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
    }
}
