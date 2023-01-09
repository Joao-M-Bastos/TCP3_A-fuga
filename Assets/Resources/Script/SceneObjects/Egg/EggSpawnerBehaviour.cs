using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawnerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject eggObj;
    [SerializeField] private int baseCooldown;
    private int cooldown;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = baseCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown < 0)
        {
            cooldown = baseCooldown;
            Instantiate(eggObj, this.transform.position, this.transform.rotation);
        }
    }


}
