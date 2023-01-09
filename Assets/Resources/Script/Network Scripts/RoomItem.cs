using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class RoomItem : MonoBehaviour
{
    public TextMeshProUGUI roomName;
    public TextMeshProUGUI roomNP;
    LobbyManager lobbyManager;


    public void setRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }

    public void SetNumberPlayers(string _roomNP)
    {
        roomNP.text =  PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }

    void Start()
    {
        lobbyManager = FindObjectOfType<LobbyManager>();
    }

    public void OnclickItem()   
    {
        lobbyManager.JoinRoom(roomName.text);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
