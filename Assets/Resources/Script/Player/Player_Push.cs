using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Push : MonoBehaviour
{
    private float pushForce = 25;
    private IPushed iPushed;
    private RagdollEffect ragdollEffect;

    public Rigidbody rb;

    private bool hasPlayerCollision;

    public void Update()
    {
        if (hasPlayerCollision)
        {
            try
            {
                if (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z) > 7)
                {
                    ragdollEffect.RagDollOn();
                }

                iPushed.ApplyForceIn(rb.transform.forward * pushForce);
            }
            catch {}
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasPlayerCollision = true;

            try
            {
                iPushed = collision.gameObject.GetComponent<IPushed>();

                ragdollEffect = collision.gameObject.GetComponent<RagdollEffect>();
            }
            catch { }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
            hasPlayerCollision = false;
    }
}
