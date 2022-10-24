using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RagdollEffect : MonoBehaviour
{
    private bool ragDollEffect, isChangingRagDoll;

    public void RagDollOn()
    {
        ragDollEffect = true;        
    }

    public IEnumerator RagDollOff()
    {
        yield return new WaitForSeconds(5f);
        this.transform.rotation = new Quaternion(0, 0, 0, 0);
        ragDollEffect = false;
        isChangingRagDoll = false;
    }

    //-------------------------------------- TESTS ----------------------------------
    
    private void OnCollisionStay(Collision colisao)
    {
        if (colisao.gameObject.CompareTag("Ground") && ragDollEffect && !isChangingRagDoll)
        {
            isChangingRagDoll = true;
            StartCoroutine(RagDollOff());
        }
    }

    public bool IsRagDoll
    {
        set { ragDollEffect = value;}
        get { return ragDollEffect;}
    }
}
