using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroncosMovement : MonoBehaviour
{
    private float lifeSpam;

    private void Start()
    {
        lifeSpam = 10f;
    }

    private void Update()
    {
        if (lifeSpam < 0)
            Destroy(this.gameObject);
        lifeSpam -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Kill")
        {
            Destroy(gameObject);
        }
    }
}
