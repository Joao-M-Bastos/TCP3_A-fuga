using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon;
using TMPro;

public class Playerpersist : MonoBehaviour
{
    public TextMeshProUGUI text;
    public static Playerpersist instance;
    

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerInfo()
    {
        Debug.Log(PhotonNetwork.NickName + "testegame");
        text.text = PhotonNetwork.NickName.ToString();
    }
    void Start()
    {
        SetPlayerInfo();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
