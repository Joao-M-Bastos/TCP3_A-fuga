using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnScrp : MonoBehaviour
{
    [SerializeField] private int lastRespawnValue;

    private void OnTriggerEnter(Collider colisao)
    {
        if (colisao.CompareTag("Kill"))
        {
            GameObject.Find("RespawnManeger").gameObject.GetComponent<RespawnMngr>().DoRespawn(this.transform, lastRespawnValue);
            this.transform.rotation = Quaternion.identity;
        }
    }

    public int LastRespawnValue
    {
        set { lastRespawnValue = value; }
        get { return lastRespawnValue; }
    }
}
