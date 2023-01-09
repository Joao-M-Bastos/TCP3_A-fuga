using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryYou : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            Player_Controller player;

            if(other.TryGetComponent<Player_Controller>(out player))
                player.playerParentTransform.SetParent(this.transform);

            EpicBot_Controller bot;

            if (other.TryGetComponent<EpicBot_Controller>(out bot))
                player.playerParentTransform.SetParent(this.transform);

        }
    }

    private void OnTriggerExit(Collider other)
    {

        Player_Controller player;

        if (other.TryGetComponent<Player_Controller>(out player))
            player.playerParentTransform.SetParent(null);

        EpicBot_Controller bot;

        if (other.TryGetComponent<EpicBot_Controller>(out bot))
            player.playerParentTransform.SetParent(null);

    }
}
