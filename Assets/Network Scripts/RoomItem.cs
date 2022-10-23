using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomItem : MonoBehaviour
{
    public TextMeshProUGUI roomName;
    LobbyManager lobbyManager;


    public void setRoomName(string _roomName)
    {
        roomName.text = _roomName;
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
