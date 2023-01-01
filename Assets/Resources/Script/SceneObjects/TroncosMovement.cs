using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroncosMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TroncosKill")
        {
            Destroy(gameObject);
        }
    }
}
