using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationX : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool rotationSide;
    void Update()
    {
        if(rotationSide)
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, 0);
        else
            transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0, 0);
    }
}
