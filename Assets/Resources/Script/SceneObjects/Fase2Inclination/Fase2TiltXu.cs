using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase2TiltXu : MonoBehaviour
{
    [SerializeField]
    GameObject disco;
    [SerializeField]
    float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            disco.transform.Rotate(speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }
    }
}
