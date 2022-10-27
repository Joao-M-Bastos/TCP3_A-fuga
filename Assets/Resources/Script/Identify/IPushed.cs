using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPushed : MonoBehaviour
{
    private Rigidbody entityRB;

    private void Awake()
    {
        entityRB = this.gameObject.GetComponentInChildren<Rigidbody>();
    }

    public void ApplyForceIn(Vector3 force)
    {
        bool isRagbollOn = true;

        if (this.gameObject.GetComponentInChildren<RagdollEffect>() != null)
        {
            isRagbollOn = this.gameObject.GetComponentInChildren<RagdollEffect>().IsRagDoll;
            Debug.Log("D");
        }

        if (isRagbollOn)
        {
            entityRB.AddForce(force);
            Debug.Log("C");
        }
    }
}
