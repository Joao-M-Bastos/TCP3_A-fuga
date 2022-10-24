using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMEnu : MonoBehaviour
{
    GameObject menuCanvas;

    public bool isMenuOn;

    private void Awake()
    {
        menuCanvas = this.gameObject.GetComponentInChildren<Canvas>().gameObject;
    }

    private void Start()
    {
        menuCanvas.SetActive(false);
        IsMenuOn = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Lobby");
            //menuCanvas.SetActive(true);
            //IsMenuOn = true;
        }
    }

    public void OnClickContinar()
    {
        //menuCanvas.SetActive(false);
        //IsMenuOn = false;
    }

    public void OnClickSair()
    {
        //IsMenuOn = false;
        
    }

    public bool IsMenuOn
    {
        set { isMenuOn = value; }
        get { return isMenuOn; }
    }
}
