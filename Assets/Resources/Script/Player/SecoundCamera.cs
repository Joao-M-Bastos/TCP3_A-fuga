using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecoundCamera : MonoBehaviour
{
    private void Awake()
    {
        //Transform cameraTranform = GameObject.FindGameObjectWithTag("SecondCamera").transform;
        //this.transform.position = cameraTranform.position;
        //this.transform.rotation = cameraTranform.rotation;
    }

    private void OnLevelWasLoaded(int level)
    {
        Transform cameraTranform = GameObject.FindGameObjectWithTag("SecondCamera").transform;
        this.transform.position = cameraTranform.position;
        this.transform.rotation = cameraTranform.rotation;
    }
}
