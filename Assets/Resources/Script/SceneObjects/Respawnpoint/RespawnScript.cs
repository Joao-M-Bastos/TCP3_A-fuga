using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public int respawnPointValue;

    public Transform[] respawnPointPositon;

    public void PutPlayer(Transform playerTransform)
    {
        playerTransform.position = respawnPointPositon[Random.Range(0, respawnPointPositon.Length)].position;
    }
}
