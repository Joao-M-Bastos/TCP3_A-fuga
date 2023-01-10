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

    private int winNumber, levelNumber, maxWinNumber, loosedPlayers;

    private List<int> mapsGone;

    GameObject[] playerObjects;

    private EpicBot_Controller t;

    public int modfierID;

    float cont;

    public bool isFaseTitanic;

    private void Awake()
    {
        StartGame();
    }

    private void StartGame()
    {
        levelNumber = 0;
        modfierID = UnityEngine.Random.Range(0, 4);
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        winNumber = 0;
        maxWinNumber = 0;
        cont = 0;
        loosedPlayers = 0;


        DeleteImpostors();//apaga novos Fase Managers criados

        IsThisFaseTitanic();//Coloca a variavel isFaseTitaic de acordo com se a fase é ou não a do titanic

        GeneratePlayers();//Gera a quantidade de jogadores e os pede para colocar em campo
    }

    private void DeleteImpostors()
    {
        GameObject[] fasesManagers = GameObject.FindGameObjectsWithTag("GameController");


        if (fasesManagers.Length > 1)
        {
            fasesManagers[1].GetComponent<FaseManager>().cont = -500;
            Destroy(fasesManagers[1].gameObject);
            if (this.cont < -450)
                return;
        }
    }

    private void GeneratePlayers()
    {
        levelNumber += 1;

        playerSpawnerScrp = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<PlayerSpawnerScrp>();


        switch (levelNumber)
        {

            case 1:
                maxWinNumber = 7;
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

    private void IsThisFaseTitanic()
    {
        if (GameObject.FindGameObjectWithTag("FASETITANIC"))
            isFaseTitanic = true;
        else
            isFaseTitanic = false;
    }

    private void Update()
    {
        if (cont > 2)
        {
            if (winNumber + loosedPlayers >= maxWinNumber || loosedPlayers == 3)
                EndGame();
        }
        else
            cont += Time.deltaTime;
    }

    public void EndGame()
    {
        winNumber = -100;
        cont = -100;

        playerObjects = GameObject.FindGameObjectsWithTag("Player");
        Player_Controller playerControler;
        foreach (GameObject player in playerObjects)
        {
            if (player.TryGetComponent<Player_Controller>(out playerControler) && playerControler.playerView.IsMine && !isFaseTitanic)
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
        yield return new WaitForSeconds(1);

        int rand = GenerateNewMap();

        while(rand == 0)
        {
            rand = GenerateNewMap();
        }

        PhotonNetwork.LoadLevel("Map" + rand);
    }

    private int GenerateNewMap()
    {
        int r = UnityEngine.Random.Range(2, 5);

        foreach(int i in mapsGone)
        {
            if(i == r)
                return 0;
        }
        mapsGone.Add(r);
        return r;
    }

    public void PlayerHasWin(GameObject playerPreFab)
    {
        Destroy(playerPreFab);

        winNumber++;
        Debug.Log(winNumber);
    }

    public override void OnLeftRoom()
    {
        ++this.loosedPlayers;

        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("LooseScreen");

        Destroy(this.gameObject);
    }

    public void LeveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void KickBot(GameObject botPreFab)
    {
        ++this.loosedPlayers;

        Destroy(botPreFab);
    }

    public int ModifierID{
        get { return modfierID; }
    }
}
