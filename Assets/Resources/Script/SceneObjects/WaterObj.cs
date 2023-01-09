using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObj : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) {
            Player_Controller player;

            if (other.gameObject.TryGetComponent<Player_Controller>(out player))
                player.isOnWater = true;
            else
                other.gameObject.GetComponent<EpicBot_Controller>().isOnWater = true;

            EpicBot_Controller bot;

            if (other.gameObject.TryGetComponent<EpicBot_Controller>(out bot))
                bot.isOnWater = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player_Controller player;
            EpicBot_Controller bot;

            if (other.gameObject.TryGetComponent<Player_Controller>(out player))
                player.isOnWater = false;
            else
                other.gameObject.GetComponent<EpicBot_Controller>().isOnWater = false;

            if (other.gameObject.TryGetComponent<EpicBot_Controller>(out bot))
                bot.isOnWater = false;
        }
    }
}
