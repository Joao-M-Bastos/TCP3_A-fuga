using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase2TiltXd : MonoBehaviour
{
    [SerializeField]
    GameObject disco;
    [SerializeField]
    float speed;

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            disco.transform.Rotate(-speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            if (disco.transform.eulerAngles.x < 340 && disco.transform.eulerAngles.x > 30)//coloquei 30 para segurança
            {

                disco.transform.eulerAngles = new Vector3(-20, 0, disco.transform.eulerAngles.z);
            }
        }
    }
}
