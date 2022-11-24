using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifiers : MonoBehaviour
{
    public static int modifier;

    // Start is called before the first frame update
    void Awake()
    {
        modifier = Random.Range(0, 2);
    }

    void Start()
    {
        if (modifier == 1) Debug.Log("Modifier Active: Extra Double Jump");
    }

    // Update is called once per frame
    void Update()
    {
        //DEBUG
        //if (Input.GetKeyDown("z")) Debug.Log(modifier);
    }
}
