using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoRagdoll : MonoBehaviour
{
    public bool hasBonkSFX;

    

    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            Player_RagdollEffect playerRagdoll = collision.gameObject.GetComponent<Player_RagdollEffect>();
            playerRagdoll.BonkSFX(hasBonkSFX);
        }
        catch { }
        
    }

    private void OnCollisionStay(Collision colisao)
    {
        try
        {
            Player_RagdollEffect playerRagdoll = colisao.gameObject.GetComponent<Player_RagdollEffect>();
            if (!playerRagdoll.IsRagDoll)
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
