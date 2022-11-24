using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSaveScpt : MonoBehaviour
{
    [SerializeField] private RespawnScript respawnScript;

    private void OnTriggerEnter(Collider colisao)
    {
        if (colisao.gameObject.CompareTag("Player"))
        {
            RespawnSave(colisao.gameObject.GetComponent<PlayerRespawnScrp>(), respawnScript.respawnPointValue);
        }
    }

    public void RespawnSave(PlayerRespawnScrp playerRespawnScrp, int newValue)
    {
        playerRespawnScrp.LastRespawnValue = newValue;
    }
}
