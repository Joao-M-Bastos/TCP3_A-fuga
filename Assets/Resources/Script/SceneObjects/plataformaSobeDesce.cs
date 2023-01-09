using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformaSobeDesce : MonoBehaviour
{
    public float speedBase;
    public float speedVariation;

    private float speed;
    private int direction;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(speedBase - speedVariation, speedBase + speedVariation);
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + (speed * Time.deltaTime * direction), transform.position.z);

        if (transform.position.y <= -5 && direction == -1)
            direction = 1;

        if (transform.position.y >= 5 && direction == 1)
            direction = -1;
    }
}
