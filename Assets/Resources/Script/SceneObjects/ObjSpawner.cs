using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjSpawner : MonoBehaviour
{
    public GameObject[] objectsPrefab;

    float createObjInstanceCooldown;

    // Update is called once per frame
    void Update()
    {
        createObjInstanceCooldown -= Random.Range(0, 20);

        if (createObjInstanceCooldown < 0)
        {
            CreateObject();
            createObjInstanceCooldown = 1200 * Random.Range(3, 5);
        }
    }

    private void CreateObject()
    {
        int objInt = Random.Range(0, objectsPrefab.Length);
        GameObject objectToSpawn = objectsPrefab[objInt];
        objectToSpawn.transform.position = this.transform.position;
        Instantiate(objectToSpawn);
    }
}
