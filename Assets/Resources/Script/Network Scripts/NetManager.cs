using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetManager : MonoBehaviourPunCallbacks
{

    public TextMeshProUGUI Statuslobby;
    public TextMeshProUGUI NomeJogador;
    public TextMeshProUGUI buttontext;

    void Start()
    {

    }

    #region Conexao Rede
    public void ButtonConnect()
    {
        if (NomeJogador.text.Length >= 1)
        {
            PhotonNetwork.NickName = NomeJogador.text;
            buttontext.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
        //SceneManager.LoadScene("MainScreen");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Voce foi desconectado por: " + cause);
    }
    #endregion
}
