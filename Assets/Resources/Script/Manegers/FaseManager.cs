using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;


public class FaseManager : MonoBehaviourPunCallbacks
{
    PlayerSpawnerScrp playerSpawnerScrp;

    private int winNumber, levelNumber, maxWinNumber;

    GameObject[] playerObjects;

    private EpicBot_Controller t;

    public int modfierID;

    float cont;

    private void Awake()
    {
        levelNumber = 0;

        StartGame();
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        winNumber = 0;
        maxWinNumber = 0;
        cont = 0;

        GameObject[] fasesManagers = GameObject.FindGameObjectsWithTag("GameController");

        if (fasesManagers.Length > 1)
        {
            fasesManagers[1].GetComponent<FaseManager>().cont = -500;
            Destroy(fasesManagers[1].gameObject);
            if (this.cont < -450)
                return;
        }

        playerSpawnerScrp = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<PlayerSpawnerScrp>();

            levelNumber += 1;

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
                case 4:
                    Win();
                    break;
            }

            playerSpawnerScrp.SpawnPlayer(maxWinNumber + 3);
    }

    private void Update()
    {
        if (winNumber >= maxWinNumber && cont > 2)
            EndGame();

        Debug.Log(winNumber);
        Debug.Log(maxWinNumber);
        cont += Time.deltaTime;
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
        winNumber = -100;
        cont = -100;

        playerObjects = GameObject.FindGameObjectsWithTag("Player");
        Player_Controller playerControler;
        foreach (GameObject player in playerObjects)
        {
            if (player.TryGetComponent<Player_Controller>(out playerControler) && playerControler.playerView.IsMine)
            {
                PhotonNetwork.LeaveRoom();
            }
        }

        StartCoroutine(NextGame()) ;
    }

    public void Win()
    {
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("WinScreen");

        Destroy(this.gameObject);
    }

    IEnumerator NextGame()
    {
        yield return new WaitForSeconds(2);
        PhotonNetwork.LoadLevel(2);
    }

    public void PlayerHasWin(GameObject playerPreFab)
    {
        Destroy(playerPreFab);

        winNumber++;
    }

    public override void OnLeftRoom()
    {
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("LooseScreen");

        Destroy(this.gameObject);
    }

    public void LeveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public int ModifierID{
        get { return modfierID; }
    }
}
