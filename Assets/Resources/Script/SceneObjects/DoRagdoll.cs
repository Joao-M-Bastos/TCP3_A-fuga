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
            RagdollEffect playerRagdoll = collision.gameObject.GetComponent<RagdollEffect>();
            playerRagdoll.BonkSFX(hasBonkSFX);
        }
        catch { }

    }

    private void OnCollisionStay(Collision colisao)
    {
        try
        {
            RagdollEffect playerRagdoll = colisao.gameObject.GetComponent<RagdollEffect>();
            if (!playerRagdoll.IsRagDoll)
                playerRagdoll.RagDollOn();
        }
        catch { }
    }

    private void OnTriggerStay(Collider colisao)
    {
        try
        {
            RagdollEffect playerRagdoll = colisao.gameObject.GetComponent<RagdollEffect>();
            if (!playerRagdoll.IsRagDoll)
                playerRagdoll.RagDollOn();
        }
        catch { }
    }

}
