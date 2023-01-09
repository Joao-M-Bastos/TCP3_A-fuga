using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase2TiltZu : MonoBehaviour
{
    [SerializeField]
    GameObject disco;
    [SerializeField]
    float speed;

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            disco.transform.Rotate(0.0f, 0.0f, -speed * Time.deltaTime, Space.World);
            if (disco.transform.eulerAngles.z < 340 && disco.transform.eulerAngles.z > 20)
            {
                disco.transform.eulerAngles = new Vector3(disco.transform.eulerAngles.x, 0, -20);
            }
        }
    }
}
