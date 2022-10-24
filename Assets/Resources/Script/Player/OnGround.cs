using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGround : MonoBehaviour
{
    public bool isOnGround;

    private void OnTriggerStay(Collider colisao)
    {
        if (colisao.gameObject.CompareTag("Ground")&& !this.isOnGround)
            this.isOnGround = true;
    }

    private void OnTriggerExit(Collider colisao)
    {
        if (this.isOnGround)
            this.isOnGround = false;
    }
}
