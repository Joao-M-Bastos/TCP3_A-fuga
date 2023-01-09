using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformaAparece : MonoBehaviour
{
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" && GetComponent<MeshRenderer>().enabled == false)
            GetComponent<MeshRenderer>().enabled = true;
    }
}
