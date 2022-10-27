using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Push
{
    private float pushForce = 25;
    private IPushed iPushed;
    private RagdollEffect ragdollEffect;

    public void PushOponent(Transform playerTransform, float speed)
    {
        Vector3 rayPosition = new Vector3(playerTransform.position.x, playerTransform.position.y - 0.25f, playerTransform.position.z);

        Debug.DrawRay(rayPosition, playerTransform.forward, Color.blue);

        if (Physics.Raycast(rayPosition, playerTransform.forward, out RaycastHit hit, 0.5f))
        {
            try
            {
                iPushed = hit.collider.gameObject.GetComponent<IPushed>();
                iPushed.ApplyForceIn(playerTransform.forward * pushForce);

                if (speed > 6)
                {
                    ragdollEffect = hit.collider.gameObject.GetComponent<RagdollEffect>();
                    ragdollEffect.RagDollOn();
                }
            }
            catch {}
        }
    }

    public void PlayerDash(Rigidbody rb)
    {
        rb.AddForce(rb.transform.forward * 300);
    }
}
