using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEffect : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipGetHit, audioClipGetHitHonk;

    private bool ragDollEffect, isChangingRagDoll, touchingGround;

    private float ragdollCount;

    public Rigidbody thisRB;

    private void Awake()
    {
        thisRB = this.gameObject.GetComponent<Rigidbody>();
    }

    public void RagDollOn()
    {
        IsRagDoll = true;
        this.gameObject.GetComponent<Collider>().material.dynamicFriction = 1;
        thisRB.freezeRotation = !IsRagDoll;
        ragdollCount = 3;
    }

    public void RagDollOff()
    {
        audioSource.PlayOneShot(audioClipGetHitHonk, 1);
        this.transform.rotation = new Quaternion(0, 0, 0, 0);
        IsRagDoll = false;
        thisRB.freezeRotation = !IsRagDoll;
        isChangingRagDoll = IsRagDoll;
        this.gameObject.GetComponent<Collider>().material.dynamicFriction = 0;
    }

    private void Update()
    {
        if (IsRagDoll && touchingGround)
        {
            if(ragdollCount < 0)
            {
                RagDollOff();
            }
            ragdollCount -= Time.deltaTime;
        }
    }

    //-------------------------------------- TESTS ----------------------------------

    private void OnCollisionStay(Collision colisao)
    {
        if (colisao.gameObject.CompareTag("Ground"))
        {
            touchingGround = true;
        }
    }

    private void OnCollisionExit(Collision colisao)
    {
        if (colisao.gameObject.CompareTag("Ground"))
        {
            touchingGround = false;
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
