using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehaviour : MonoBehaviour
{
    private float lifeSpam;

    private void Start()
    {
        lifeSpam = 15;
    }

    private void Update()
    {
        if (lifeSpam < 0)
            Destroy(this.gameObject);
        lifeSpam += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("KILL"))
            Destroy(this.gameObject);

    }
}
