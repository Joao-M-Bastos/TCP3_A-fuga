using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroncosSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject troncosManager;
    [SerializeField]
    private GameObject troncoPrefab;
    private float randomz;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnTronco", 0.25f, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnTronco()
    {
        randomz = Random.Range(troncosManager.transform.position.z - 12.5f, troncosManager.transform.position.z + 12.5f);
        Instantiate(troncoPrefab, new Vector3(troncosManager.transform.position.x, troncosManager.transform.position.y, randomz), Quaternion.identity);
    }
}
