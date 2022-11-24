using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicBot_JumpTrigger : MonoBehaviour
{
    [SerializeField] EpicBot_Controller epicBot_Controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DoRagdoll"))
        {
            
            epicBot_Controller.spaceIsPressed = true;
        }
    }
}
