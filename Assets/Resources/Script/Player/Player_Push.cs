using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Push : MonoBehaviour
{
    public RagdollEffect thisRagdollEffect;

    public Rigidbody rb;

    public void OnTriggerStay(Collider collision)
    {
            if (collision.gameObject.CompareTag("Player"))
            {
                RagdollEffect playerRagdoll = collision.gameObject.GetComponent<RagdollEffect>();
                if (!playerRagdoll.IsRagDoll && !thisRagdollEffect.IsRagDoll && Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z) > 7.5f)
                    playerRagdoll.RagDollOn();
            }
    }
}
