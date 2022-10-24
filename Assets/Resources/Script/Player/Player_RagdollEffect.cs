using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RagdollEffect : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipGetHit, audioClipGetHitHonk;

    private bool ragDollEffect, isChangingRagDoll;

    public void RagDollOn()
    {
        ragDollEffect = true;
    }

    public IEnumerator RagDollOff()
    {
        yield return new WaitForSeconds(5f);
        audioSource.PlayOneShot(audioClipGetHitHonk, 1);
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

    public void BonkSFX(bool hasSound)
    {
        if (hasSound)
            audioSource.PlayOneShot(audioClipGetHit, 0.5f);
    }

    public bool IsRagDoll
    {
        set { ragDollEffect = value;}
        get { return ragDollEffect;}
    }
}
