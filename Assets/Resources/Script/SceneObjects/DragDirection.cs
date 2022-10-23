using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDirection : MonoBehaviour
{
    [SerializeField] private float dragSpeed;
    [SerializeField] private Rigidbody conveyorRigidBody;

    private void Awake()
    {
        conveyorRigidBody = this.gameObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision collision)
    {
        Vector3 pos = conveyorRigidBody.position;
        collision.transform.position += this.transform.forward * dragSpeed * Time.fixedDeltaTime;
        conveyorRigidBody.MovePosition(pos);
    }
}
