using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomhat : MonoBehaviour
{
    [SerializeField] GameObject[] hats;
    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0, hats.Length);
        hats[r].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
