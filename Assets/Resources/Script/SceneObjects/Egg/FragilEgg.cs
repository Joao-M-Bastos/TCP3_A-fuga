using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragilEgg : MonoBehaviour
{
    private float lifeSpam;

    private void Start()
    {
        lifeSpam = 5f;
    }

    private void Update()
    {
        if (lifeSpam < 0)
            Destroy(this.gameObject);
        lifeSpam -= Time.deltaTime;
    }
}
