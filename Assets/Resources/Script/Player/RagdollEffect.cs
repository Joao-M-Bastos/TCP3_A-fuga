using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEffect : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipGetHit, audioClipGetHitHonk;

    public bool doragdollll, undoragdolllll;

    private bool ragDollEffect, isChangingRagDoll, touchingGround;

    private float ragdollCount;

    Rigidbody thisRB;
    CapsuleCollider thisCapsule;
    [SerializeField] Animator thisAnimator;

    [SerializeField] CapsuleCollider[] bonesCapsules;
    [SerializeField] BoxCollider[] bonesBoxes;
    [SerializeField] Rigidbody[] bonesRigidBodies;

    private void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        thisRB = this.gameObject.GetComponent<Rigidbody>();
        thisCapsule = this.gameObject.GetComponent<CapsuleCollider>();
    }

    public void RagDollOn()
    {
        IsRagDoll = true;
        this.gameObject.GetComponent<Collider>().material.dynamicFriction = 1;

        TurnPhiphiscs(true);

        ragdollCount = 3;
    }

    public void RagDollOff()
    {
        audioSource.PlayOneShot(audioClipGetHitHonk, 1);
        this.transform.rotation = new Quaternion(0, 0, 0, 0);
        IsRagDoll = false;

        TurnPhiphiscs(false);

        isChangingRagDoll = IsRagDoll;
        this.gameObject.GetComponent<Collider>().material.dynamicFriction = 0;
    }

    private void TurnPhiphiscs(bool v)
    {
        if (thisAnimator != null && false)
        {
            thisAnimator.enabled = !v;
            thisRB.isKinematic = v;
            thisCapsule.isTrigger = v;

            foreach (Rigidbody rb in bonesRigidBodies)
            {
                rb.isKinematic = !v;
                rb.useGravity = v;
            }

            foreach (CapsuleCollider cc in bonesCapsules)
                cc.enabled = v;

            foreach (BoxCollider bc in bonesBoxes)
                bc.enabled = v;
        }
        else
        {
            thisRB.freezeRotation = !IsRagDoll;
        }
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

        if (doragdollll)
        {
            RagDollOn();
            doragdollll = false;
        }

        if (undoragdolllll)
        {
            RagDollOff();
            undoragdolllll = false;
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
