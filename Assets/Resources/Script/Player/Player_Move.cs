using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{

    //------------------------------------------- VARIAVEIS ---------------------------------------
    private float AxisX, AxisY;

    private Rigidbody playerRB;
    private LayerMask groundLayerMask;

    [SerializeField] private Animator wingAnimator;
    [SerializeField] private Transform cam, onGroundPointA, onGroundPointB;
    [SerializeField] private float frictionValue, playerBaseSpeed;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipStep1, audioClipStep2, audioClipGetHit, audioClipGetHitHonk, audioClipJump;

    [SerializeField] private float maxDoubleJumpCount;

    private bool isWingsOpen, stepFirstSound;
    
    public float playerSpeed, playerJumpForce, playerPlaneValue, stepSoundDelay;

    private float turnSmoothTime, turnSmoothVelocity, doubleJumpCount;

    public bool ragDollEffect;

    private void Awake()
    {
        StartValues();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateValues();

        DoRagdollEffect();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(audioClipJump, 0.8f);
            Jump(JumpType());
        }
    }

    private void FixedUpdate()
    {
        if (OnGround())
            Debug.Log("A");
        else
            Debug.Log("b");

        if (CanMove()) Move();

        if (this.playerRB.velocity.magnitude > 0.1f && stepSoundDelay <= 0 && OnGround() && ragDollEffect == false)
        {
            if (stepFirstSound)
            {
                audioSource.PlayOneShot(audioClipStep1, 1f);
                stepFirstSound = false;
                stepSoundDelay = 16;
            }
            else
            {
                audioSource.PlayOneShot(audioClipStep2, 1f);
                stepFirstSound = true;
                stepSoundDelay = 16;
            }
        }

        if (stepSoundDelay > 0) { stepSoundDelay -= 1; }
    }

    //--------------------------------- UPDATE INSTANCES --------------------------------

    public void StartValues()
    {
        this.groundLayerMask = LayerMask.GetMask("Ground");
        this.playerRB = this.GetComponent<Rigidbody>();
        this.turnSmoothTime = 0.1f;
        this.isWingsOpen = false;
        this.stepSoundDelay = 0f;
        this.stepFirstSound = true;
        UpdateSpeed();
    }

    public void UpdateValues()
    {
        if (OnGround() && doubleJumpCount != maxDoubleJumpCount) doubleJumpCount = maxDoubleJumpCount;
    }

    public void UpdateSpeed()
    {
        this.playerSpeed = this.playerBaseSpeed;
    }

    public void Friction()
    {
        Vector3 playerActualSpeed;
        playerActualSpeed = this.playerRB.velocity;

        if (Mathf.Abs(playerActualSpeed.x) > 1)
            playerActualSpeed.x -= Mathf.Sign(playerActualSpeed.x) * frictionValue * Time.deltaTime;
        else
            playerActualSpeed.x = 0;
        
        if (Mathf.Abs(playerActualSpeed.z) > 1)
            playerActualSpeed.z -= Mathf.Sign(playerActualSpeed.z) * frictionValue * Time.deltaTime;
        else
            playerActualSpeed.z = 0;

        this.playerRB.velocity = playerActualSpeed;
    }

    //----------------------------------------- ACTIONS -----------------------------------

    public void Move()
    {
        
        Vector3 direction;

        AxisX = Input.GetAxis("Horizontal");
        AxisY = Input.GetAxis("Vertical");

        direction = new Vector3(AxisX, 0, AxisY).normalized;

        Walk(direction);

        Fly();
    }

    public void Walk(Vector3 direction)
    {
        if (direction.magnitude >= 0.1f)
        {
            if (OnGround())
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * playerSpeed;

                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                moveDir.y = this.playerRB.velocity.y;

                this.playerRB.velocity = moveDir;

            }
            else if (isWingsOpen)
            {
                Vector3 moveDir = this.transform.forward;

                moveDir.y = playerRB.velocity.y;

                this.transform.Rotate(new Vector3(0, AxisX * 40, 0) * Time.deltaTime);

                this.playerRB.velocity = moveDir * playerSpeed;
            }
        }
        else
        {
            Friction();
        }
    }

    public void Jump(float jumpType)
    {
        switch (jumpType)
        {
            case 0:
                break;
            case 1:
                this.playerRB.velocity = new Vector3(playerRB.velocity.x, playerJumpForce, playerRB.velocity.z);
                //this.playerRB.AddForce(Vector3.up * playerJumpForce, ForceMode.Impulse);
                break;
            case 2:
                this.playerRB.velocity = new Vector3(playerRB.velocity.x, playerJumpForce / 1.6f, playerRB.velocity.z);
                doubleJumpCount--;
                break;
        }
    }

    public void DoRagdollEffect()
    {
        if (ragDollEffect)
        {
            this.playerRB.freezeRotation = false;
        }
        else this.playerRB.freezeRotation = true;
    }

    public void Fly()
    {
        isWingsOpen = CanOpenWings();
        this.wingAnimator.SetBool("OpenWings", isWingsOpen);

        if (isWingsOpen)
        {
            this.playerRB.velocity = new Vector3(playerRB.velocity.x, -playerPlaneValue, playerRB.velocity.z);
        }
    }

    public IEnumerator RagDollOff()
    {
        yield return new WaitForSeconds(5f);
        this.playerRB.velocity = new Vector3(0, 0, 0);
        ragDollEffect = false;
    }

    public void RagDollOn()
    {
        this.transform.rotation = new Quaternion(0, 0, 0, 0);
        ragDollEffect = true;
    }

    //-------------------------------------- TESTS ----------------------------------

    private void OnCollisionEnter(Collision colisao)
    {
        if (colisao.gameObject.CompareTag("DoRagdoll") && !ragDollEffect)
        {
            audioSource.PlayOneShot(audioClipGetHit, 0.8f);
            audioSource.PlayOneShot(audioClipGetHitHonk, 1);

            Debug.Log("A");
            RagDollOn();
        }
        else if (colisao.gameObject.CompareTag("Ground") && ragDollEffect)
        {
            Debug.Log("B");
            StartCoroutine(RagDollOff());
        }
    }




public bool OnGround()
    {
        if (Physics.Raycast(onGroundPointA.position, -Vector3.up, 0.01f, groundLayerMask)) return true;
        if (Physics.Raycast(onGroundPointB.position, -Vector3.up, 0.01f, groundLayerMask)) return true;
        return false;
    }

    public float JumpType()
    {
        if (OnGround()) return 1;
        else if (doubleJumpCount > 0) return 2;
        return 0;
    }

    public bool CanMove()
    {
        if (!ragDollEffect)
        {
            return true;
        }
        return false;
    }

    public bool CanOpenWings()
    {
        if (Input.GetKey(KeyCode.Space) && !OnGround() && this.playerRB.velocity.y < -playerPlaneValue + 0.01f) return true;
        return false;
    }

}


