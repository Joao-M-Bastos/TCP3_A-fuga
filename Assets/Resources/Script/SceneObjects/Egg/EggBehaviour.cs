using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehaviour : MonoBehaviour
{
    private float lifeSpam;

    private void Start()
    {
        lifeSpam = 20f;
    }

    private void Update()
    {
        if (lifeSpam < 0)
            Destroy(this.gameObject);
        lifeSpam -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Kill"))
            Destroy(this.gameObject);

    }
}
