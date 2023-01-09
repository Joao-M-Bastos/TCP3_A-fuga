using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryYou : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            other.transform.SetParent(this.transform);}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.transform.SetParent(other.GetComponent<Player_Controller>().playerParentTransform);
    }
}
