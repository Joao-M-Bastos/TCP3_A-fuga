using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseManager : MonoBehaviour
{
    PlayerSpawnerScrp playerSpawnerScrp;

    private void OnLevelWasLoaded(int level)
    {
        playerSpawnerScrp = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<PlayerSpawnerScrp>();
        playerSpawnerScrp.SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuitPlayer(GameObject playerPreFab)
    {
        Destroy(playerPreFab);
        Cursor.lockState = CursorLockMode.None;
        PhotonNetwork.LoadLevel("LobbyMultiplayer");
    }
}
