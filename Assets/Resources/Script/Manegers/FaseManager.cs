using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaseManager : MonoBehaviourPunCallbacks
{
    PlayerSpawnerScrp playerSpawnerScrp;

    private int winNumber, levelNumber, maxWinNumber;

    GameObject[] playerObjects;

    private EpicBot_Controller t;

    public int modfierID;

    private void Awake()
    {
        winNumber = 0;
        levelNumber = 0;
        maxWinNumber = 0;

        StartGame();

        DontDestroyOnLoad(this.gameObject);
    }


    private void OnLevelWasLoaded(int level)
    {
        playerSpawnerScrp = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<PlayerSpawnerScrp>();

        GameObject[] faseObjects = GameObject.FindGameObjectsWithTag("GameController");

        while(faseObjects.Length > 1)
        {
            Destroy(faseObjects[1]);
            faseObjects = GameObject.FindGameObjectsWithTag("GameController");
        }

        levelNumber -= -1;
        winNumber = 0;

        switch (levelNumber)
        {
            case 1:
                maxWinNumber = 8;
                break;
            case 2:
                maxWinNumber = 4;
                break;
            case 3:
                maxWinNumber = 1;
                break;


        }

        playerSpawnerScrp.SpawnPlayer(maxWinNumber + 3);
    }

    private void StartGame()
    {
        GerarModificadores();
    }

    private void GerarModificadores()
    {
        modfierID = UnityEngine.Random.Range(0, 4);
    }

    public void EndGame()
    {
        playerObjects = GameObject.FindGameObjectsWithTag("Player");

        bool hasWin;

        foreach(GameObject player in playerObjects)
            if (player.GetComponent<PhotonView>().IsMine)
                    PhotonNetwork.LeaveRoom();

        NextGame();
    }

    IEnumerator NextGame()
    {
        yield return new WaitForSeconds(4);
        PhotonNetwork.LoadLevel(UnityEngine.Random.Range(2, 4));
    }

    public void PlayerHasWin(GameObject playerPreFab)
    {

        Destroy(playerPreFab);

        winNumber++;

        //if (winNumber >= maxWinNumber)
            //EndGame();
    }

    public override void OnLeftRoom()
    {

        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(1);

        base.OnLeftRoom();
    }

    public void LeveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public int ModifierID{
        get { return modfierID; }
    }
}
