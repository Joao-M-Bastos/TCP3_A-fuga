using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoRagdoll : MonoBehaviour
{
    private void OnCollisionStay(Collision colisao)
    {
        try
        {
            Player_RagdollEffect playerRagdoll = colisao.gameObject.GetComponent<Player_RagdollEffect>();
            if(!playerRagdoll.IsRagDoll)
                playerRagdoll.RagDollOn();
        }
        catch { }
    }

    private void OnTriggerStay(Collider colisao)
    {
        try
        {
            Player_RagdollEffect playerRagdoll = colisao.gameObject.GetComponent<Player_RagdollEffect>();
            if (!playerRagdoll.IsRagDoll)
                playerRagdoll.RagDollOn();
        }
        catch { }
    }

}
