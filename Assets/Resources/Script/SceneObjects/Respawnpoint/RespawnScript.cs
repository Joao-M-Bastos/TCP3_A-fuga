using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public int respawnPointValue;

    public Transform[] respawnPointPositon;

    public void PutPlayer(Transform playerTransform)
    {
        int rand = Random.Range(0, respawnPointPositon.Length);
        playerTransform.position = respawnPointPositon[rand].position;
        playerTransform.rotation = respawnPointPositon[rand].rotation;
    }
}
