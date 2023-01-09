using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroncosMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.rotation.x != 0)
        {
            this.transform.rotation *= new Quaternion(0,1,1,1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TroncosKill")
        {
            Destroy(gameObject);
        }
    }
}
