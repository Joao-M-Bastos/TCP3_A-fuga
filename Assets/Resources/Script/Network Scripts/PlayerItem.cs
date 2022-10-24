using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerName;

    public GameObject leftArrow;
    public GameObject rightArrow;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    public Image playerAvatar;
    public Sprite[] avatars;

    Player player;

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
        player = _player;
        UpdatePlayerItem(player);
    }
    
    public void ApplyLocalChanges()
    {
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLeftButtonClick()
    {
        if((int)playerProperties["playerAvatar"] == 0)
            playerProperties["playerAvatar"] = avatars.Length - 1;
        else
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnRightButtonClick()
    {
        if ((int)playerProperties["playerAvatar"] == avatars.Length - 1)
            playerProperties["playerAvatar"] = 0;
        else
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(targetPlayer == player)
        {
            UpdatePlayerItem(player);
        }
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
    }

    private void UpdatePlayerItem(Player player)
    {
        if (player.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerAvatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
        }
        else
            playerProperties["playerAvatar"] = 0;
    }
}
