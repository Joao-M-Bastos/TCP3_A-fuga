using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDirection : MonoBehaviour
{
    [SerializeField] private float dragSpeed;
    [SerializeField] private Rigidbody conveyorRigidBody;
    [SerializeField] private bool isRiver;

    private void Awake()
    {
        conveyorRigidBody = this.gameObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision collision)
    {
        Vector3 pos = conveyorRigidBody.position;
        if(isRiver && collision.gameObject.tag == "Player")
            collision.transform.position += -this.transform.right * dragSpeed/2 * Time.fixedDeltaTime;
        else
            collision.transform.position += this.transform.forward * dragSpeed * Time.fixedDeltaTime;
        conveyorRigidBody.MovePosition(pos);
    }
}
