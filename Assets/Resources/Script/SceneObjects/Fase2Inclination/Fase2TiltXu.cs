using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase2TiltXu : MonoBehaviour
{
    [SerializeField]
    GameObject disco;
    [SerializeField]
    float speed;

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            disco.transform.Rotate(speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            if (disco.transform.eulerAngles.x > 20 && disco.transform.eulerAngles.x < 330)
            {
                disco.transform.eulerAngles = new Vector3(20, 0, disco.transform.eulerAngles.z);
            }
        }
    }
}
