using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    public FaseManager faseManager;

    private void Awake()
    {
        this.faseManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<FaseManager>();
    }

    private void OnLevelWasLoaded(int level)
    {
        this.faseManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<FaseManager>();
    }

    private void OnTriggerEnter(Collider colisao)
    {
        if (colisao.gameObject.CompareTag("Player"))
        {
            faseManager.QuitPlayer(colisao.gameObject);
        }
    }
}
