using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eggSpawner : MonoBehaviour
{
    public GameObject eggPrefab;
    public float spawnIntervalBase;
    public float spawnIntervalVariation;
    public float eggRotationBase;
    public float eggRotationVariation;
    public float eggForceBase;
    public float eggForceVariation;
    public float horizontalVariation;

    private float spawnInterval;
    private float eggRotation;
    private float eggForce;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;

        spawnInterval = Random.Range(spawnIntervalBase - spawnIntervalVariation, spawnIntervalBase + spawnIntervalVariation);
    }

    // Update is called once per frame
    void Update()
    {
        spawnInterval -= Time.deltaTime;

        if (spawnInterval <= 0)
        {
            GameObject spawnedEgg = null;

            spawnInterval = Random.Range(spawnIntervalBase - spawnIntervalVariation, spawnIntervalBase + spawnIntervalVariation);
            eggRotation = Random.Range(eggRotationBase - eggRotationVariation, eggRotationBase + eggRotationVariation);
            eggForce = Random.Range(eggForceBase - eggForceVariation, eggForceBase + eggForceVariation);

            spawnedEgg = Instantiate(eggPrefab, transform.position, Quaternion.identity);
            spawnedEgg.transform.position = new Vector3(spawnedEgg.transform.position.x, spawnedEgg.transform.position.y, spawnedEgg.transform.position.z + Random.Range(-horizontalVariation, horizontalVariation));
            spawnedEgg.transform.Rotate(0f, transform.eulerAngles.y, 0f, Space.World);
            spawnedEgg.GetComponent<Rigidbody>().AddForce(transform.forward * eggForce);
            spawnedEgg.GetComponent<Rigidbody>().AddTorque(transform.right * eggRotation);
        }
    }
}
