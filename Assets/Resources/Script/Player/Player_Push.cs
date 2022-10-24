using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Push : MonoBehaviour
{
    private void OnTriggerStay(Collider colisao)
    {
        if (colisao.gameObject.CompareTag("Player"))
        {
            Player_Move playerMove = colisao.gameObject.GetComponent<Player_Move>();
            if(playerMove != this.GetComponent<Player_Move>())
            {
                playerMove.ApplyForceIn(this.transform.forward);
            }
        }
    }

    private void OnTriggerExit(Collider colisao)
    {
        
    }
}
