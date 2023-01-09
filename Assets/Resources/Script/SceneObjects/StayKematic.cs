using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayKematic : MonoBehaviour
{
    Rigidbody thisRB;

    private void Awake()
    {
        this.thisRB = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.thisRB.isKinematic)
            this.thisRB.isKinematic = true;
    }
}
