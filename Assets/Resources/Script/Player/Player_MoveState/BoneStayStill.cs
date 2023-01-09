using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneStayStill : MonoBehaviour
{
    Vector3 position;
    Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        rotation = this.transform.rotation;
        position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = rotation;
        this.transform.position = position;
    }
}
