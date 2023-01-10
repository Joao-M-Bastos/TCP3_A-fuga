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
    public GameObject layoutSuperior;
    public TextMeshProUGUI layoutSuperiorText;
    public GameObject telaInicial;
    public GameObject skins;
    public GameObject controles;
    public GameObject configuracoes;
    public GameObject Multiplayer;
    public GameObject Singleplayer;
    public GameObject Multiplayer_telas;
    public GameObject Singleplayer_cenario;

    public Button buttonSingleplayer;
    public Button buttonMultiplayer;
    public Button buttonMultiplayer_telas;
    public Button buttonSingleplayer_cenario;
    public Button buttonHome;
    public Button buttonSkins;
    public Button buttonConfig;
    public Button buttonInfo;
    public Button buttonVoltar;
    public Button buttonVolta_singleplayer;
    public Button buttonVoltar_singleplayer_cenario;
    public Button buttonVoltar_multiplayer;
    public Button buttonVoltar_multiplayer_telas;
    public Button buttonjogar_controle;
    public Button buttonJogar;

    public TMP_InputField roomInputField;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public GameObject menuPrincipal;
    public GameObject lobbySelection;
    public GameObject gameSelection;
    public GameObject listRoomScreen;
    public GameObject room;
    public TextMeshProUGUI roomName;
    public TextMeshProUGUI roomNP;

    public RoomItem roomItemPrefab;
    List<RoomItem> roomItemsList = new List<RoomItem>();
    public Transform contentObject;

    public float timeBetweenUpdates = 1.5f;
    float nextupdatetime;

    public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;

    public GameObject playbutton;
    GameObject last_tela;

    private void Awake()
    {
        layoutSuperior.SetActive(true);
        telaInicial.SetActive(true);
        buttonHome.enabled = false;
        buttonSkins.enabled = true;
        buttonConfig.enabled = true;
        buttonInfo.enabled = true;
        last_tela = telaInicial;
        buttonJogar.enabled = true;
        buttonVoltar.enabled = false;
    }

    public void setnamesuperiortext(string texto)
    {
        layoutSuperiorText.text = texto.ToString();
    }

    #region SinglePlayer_Mapas
    public void corridaGranja()
    {
        SceneManager.LoadScene("mapa1");
    }

    public void Titanic()
    {
        SceneManager.LoadScene("mapa2");
    }

    public void EscapePrisao()
    {
        SceneManager.LoadScene("mapa3");
    }

    public void geb()
    {
        SceneManager.LoadScene("mapa4");
    }

    public void circuito()
    {
        SceneManager.LoadScene("Circuito");
    }

    #endregion
    public void Menu_Inicial()
    {
        setnamesuperiortext("INICIO");
        last_tela.SetActive(false);
        telaInicial.SetActive(true);
        buttonHome.enabled = false;
        buttonSkins.enabled = true;
        buttonConfig.enabled = true;
        buttonInfo.enabled = true;
        last_tela = telaInicial;
        buttonJogar.enabled = true;
        buttonVoltar.enabled = false;
    }


    public void Menu_Skins()
    {
        setnamesuperiortext("SKINS");
        last_tela.SetActive(false);
        skins.SetActive(true);
        buttonHome.enabled = true;
        buttonSkins.enabled = false;
        buttonConfig.enabled = true;
        buttonInfo.enabled = true;
        last_tela = skins;
    }

    public void Menu_Info()
    {
        setnamesuperiortext("CONTROLES");
        last_tela.SetActive(false);
        controles.SetActive(true);
        buttonHome.enabled = true;
        buttonSkins.enabled = true;
        buttonConfig.enabled = true;
        buttonInfo.enabled = false;
        last_tela = controles;
    }

    public void telaconfig()
    {
        setnamesuperiortext("MENU");
        last_tela.SetActive(false);
        configuracoes.SetActive(true);
        buttonHome.enabled = true;
        buttonSkins.enabled = true;
        buttonConfig.enabled = false;
        buttonInfo.enabled = true;
        last_tela = configuracoes;
        buttonjogar_controle.enabled = true;
    }

    public void tela_Singleplayer()
    {
        setnamesuperiortext("Saguao");
        last_tela.SetActive(false);
        Singleplayer.SetActive(true);
        buttonHome.enabled = true;
        buttonSkins.enabled = true;
        buttonConfig.enabled = true;
        buttonInfo.enabled = true;
        last_tela = Singleplayer;
        buttonMultiplayer.enabled = true;
        buttonSingleplayer.enabled = false;
        buttonJogar.enabled = false;
        buttonVolta_singleplayer.enabled = true;
    }

    public void tela_Singleplayer_mapas()
    {
        setnamesuperiortext("MAPAS");
        last_tela.SetActive(false);
        Singleplayer_cenario.SetActive(true);
        buttonHome.enabled = true;
        buttonSkins.enabled = true;
        buttonConfig.enabled = true;
        buttonInfo.enabled = true;
        last_tela = Singleplayer_cenario;
        buttonJogar.enabled = false;
        buttonVoltar_singleplayer_cenario.enabled = true;
    }

    public void tela_Multiplayer()
    {
        setnamesuperiortext("Saguao");
        last_tela.SetActive(false);
        Multiplayer.SetActive(true);
        buttonHome.enabled = true;
        buttonSkins.enabled = true;
        buttonConfig.enabled = true;
        buttonInfo.enabled = true;
        last_tela = Multiplayer;
        buttonMultiplayer.enabled = false;
        buttonSingleplayer.enabled = true;
        buttonJogar.enabled = false;
        buttonVoltar_multiplayer.enabled = true;
}

    public void tela_Multiplayer_selection()
    {
        setnamesuperiortext("SALAS");
        last_tela.SetActive(false);
        Multiplayer_telas.SetActive(true);
        buttonHome.enabled = true;
        buttonSkins.enabled = true;
        buttonConfig.enabled = true;
        buttonInfo.enabled = true;
        last_tela = Multiplayer_telas;
        buttonMultiplayer.enabled = false;
        buttonSingleplayer.enabled = true;
        buttonJogar.enabled = false;
        buttonVoltar_multiplayer_telas.enabled = true;
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
        PhotonNetwork.JoinLobby();
    }

    /*public void clickCreateRoom()
    {
        if (roomInputField.text.Length >= 1)
        {

            string nome_jogador = ("Sala de: " + PhotonNetwork.NickName);

            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions() { MaxPlayers = 2 });
        }
    }*/

    public void clickCreateRoom()
    {
            string nome_jogador = ("Sala de: " + PhotonNetwork.NickName);
            PhotonNetwork.CreateRoom(nome_jogador, new RoomOptions() { MaxPlayers = 16 });
    }



    public override void OnJoinedRoom()
    {
        Multiplayer.SetActive(false);
        Multiplayer_telas.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextupdatetime)
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
            newroom.SetNumberPlayers(room.PlayerCount.ToString());
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
        Multiplayer.SetActive(true);
        roomPanel.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    void UpdatePlayerList()
    {
        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();
        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newplayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newplayerItem.SetPlayerInfo(player.Value);
            playerItemsList.Add(newplayerItem);
        }
    }

    public void GetCurrentRoomPlayers()
    {
        int contador;
        roomNP.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }




    void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 1)
        {
            playbutton.SetActive(true);
        }
        else
        {
            playbutton.SetActive(false);
        }
    }
    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel("Map1");
    }

    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        base.OnLobbyStatisticsUpdate(lobbyStatistics);
    }

}