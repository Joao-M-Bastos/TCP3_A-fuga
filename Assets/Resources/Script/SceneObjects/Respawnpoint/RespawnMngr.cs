using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnMngr : MonoBehaviour
{
    [SerializeField] private RespawnScript[] respawnScripts;

    public void DoRespawn(Transform playerTransform, int lastRespawnValue)
    {
        respawnScripts[lastRespawnValue].PutPlayer(playerTransform);
    }
}
