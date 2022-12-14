using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomInputField;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public GameObject menuPrincipal;
    public GameObject lobbySelection;
    public GameObject gameSelection;
    public GameObject listRoomScreen;
    public GameObject room;
    public GameObject gameManagerPre;
    public TextMeshProUGUI roomName;

    public RoomItem roomItemPrefab;
    List<RoomItem> roomItemsList = new List<RoomItem>();
    public Transform contentObject;

    public float timeBetweenUpdates = 1.5f;
    float nextupdatetime;

    public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;

    public GameObject playbutton;

    private void Awake()
    {
        menuPrincipal.SetActive(true);
    }

    public void testeroom()
    {
        gameSelection.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public void playButton()
    {
        menuPrincipal.SetActive(false);
        lobbySelection.SetActive(true);
    }

    public void voltarButtonLobby()
    {
        menuPrincipal.SetActive(true);
        lobbySelection.SetActive(false);
    }

    public void voltarmultiplayerButton()
    {
        lobbySelection.SetActive(true);
        gameSelection.SetActive(false);
    }

    public void multiplayerButton()
    {
        lobbySelection.SetActive(false);
        gameSelection.SetActive(true);
    }

    public void voltarGameSelection()
    {
        lobbySelection.SetActive(true);
        gameSelection.SetActive(false);
    }
    public void joinButton()
    {
        gameSelection.SetActive(false);
        listRoomScreen.SetActive(true);
    }

    public void voltarListRoom()
    {
        listRoomScreen.SetActive(false);
        gameSelection.SetActive(true);
    }

    private void Start()
    {
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();

        if(!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }

    public void clickCreateRoom()
    {
        if (roomInputField.text.Length > 1)
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions() { MaxPlayers = 5 });
        }
    }



    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >= nextupdatetime)
        {
            UpdateRoomList(roomList);
            nextupdatetime = Time.time + timeBetweenUpdates;
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Seu erro foi: " + returnCode + message);
        SceneManager.LoadScene("Lobby");
    }
    public void onClickRandomRoom()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach (RoomInfo room in list)
        {
            RoomItem newroom = Instantiate(roomItemPrefab, contentObject);
            newroom.setRoomName(room.Name);
            roomItemsList.Add(newroom);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeftRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    void UpdatePlayerList()
    {
        foreach(PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();
        if(PhotonNetwork.CurrentRoom == null)
        {
            return;
        }
        foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newplayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newplayerItem.SetPlayerInfo(player.Value);
            playerItemsList.Add(newplayerItem);
        }
    }
    void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 1)
        {
            playbutton.SetActive(true);
        } else
        {
            playbutton.SetActive(false);
        }
    }
    public void OnClickPlayButton()
    {
        Instantiate(gameManagerPre, Vector3.zero, Quaternion.identity);
        PhotonNetwork.LoadLevel("Map1");
    }

    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        base.OnLobbyStatisticsUpdate(lobbyStatistics);
    }
}
