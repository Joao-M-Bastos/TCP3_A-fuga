using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Move : MonoBehaviour
{

    //---------------------------------- VARIAVEIS -----------------------------------------
    private OnGround onGoundInstance;
    private RagdollEffect playerRedDoll;
    private PlayerRespawnScrp playerRespawnScrp;

    private HasWallOnFront hasWall;

    private float AxisX, AxisY;

    private Rigidbody playerRB;

    [SerializeField] private Animator gooseAnimator;
    [SerializeField] private Transform cam;
    [SerializeField] private float frictionValue, playerBaseSpeed, stepSoundDelay;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipJump;
    [SerializeField] private AudioClip[] audioClipSteps;

    [SerializeField] private float maxDoubleJumpCount;

    private bool isWingsOpen;

    public float playerSpeed, playerJumpForce, playerPlaneValue;

    private float turnSmoothTime, turnSmoothVelocity, doubleJumpCount, dashCoolDown;

    private PhotonView playerView;

    Vector3 moveDirection;

    private void Awake()
    {
        onGoundInstance = GetComponentInChildren<OnGround>();
        hasWall = new HasWallOnFront();
        playerView = GetComponent<PhotonView>();
        playerRespawnScrp = GetComponent<PlayerRespawnScrp>();
        this.playerRB = this.GetComponent<Rigidbody>();
        playerRedDoll = this.GetComponent<RagdollEffect>();
    }

    private void Start()
    {
        if (!playerView.IsMine)
            DestoyExtra();
        else
            StartValues();

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DestoyExtra()
    {
        Destroy(GetComponentInChildren<Camera>().gameObject);
        Destroy(GetComponentInChildren<Cinemachine.CinemachineCollider>().gameObject);
        Destroy(playerRB);
        Destroy(onGoundInstance);
        Destroy(playerRedDoll);
        Destroy(playerRespawnScrp);
    }

    public void StartValues()
    {
        UpdateSpeed();
        this.turnSmoothTime = 0.05f;
        this.stepSoundDelay = 5f;
        this.isWingsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerView.IsMine)
        {
            UpdateValues();

            if (!playerRedDoll.IsRagDoll)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    Jump(JumpType());

                if (Input.GetKeyDown(KeyCode.LeftShift) && dashCoolDown < 0 && onGoundInstance.isOnGround)
                {
                    this.PlayerDash();
                    dashCoolDown = 1.5f;
                }
                else
                    dashCoolDown -= Time.deltaTime;
            }
            DoRagdollEffect();
        }
    }

    private void FixedUpdate()
    {
        if (playerView.IsMine)
        {
            if (CanMove()) Move();
        }
    }

    //--------------------------------- UPDATE INSTANCES --------------------------------

    public void UpdateValues()
    {
        if (onGoundInstance.isOnGround && doubleJumpCount != maxDoubleJumpCount) doubleJumpCount = maxDoubleJumpCount;
    }

    public void UpdateSpeed()
    {
        this.playerSpeed = this.playerBaseSpeed;
    }

    public void Friction()
    {
        Vector3 playerActualSpeed = playerRB.velocity;

        if (Mathf.Abs(playerActualSpeed.x) > 5)
            playerActualSpeed.x -= Mathf.Sign(playerActualSpeed.x) * frictionValue * Time.deltaTime;
        else
            playerActualSpeed.x = 0;

        if (Mathf.Abs(playerActualSpeed.z) > 5)
            playerActualSpeed.z -= Mathf.Sign(playerActualSpeed.z) * frictionValue * Time.deltaTime;
        else
            playerActualSpeed.z = 0;

        this.playerRB.velocity = playerActualSpeed;
    }

    //----------------------------------------- ACTIONS -----------------------------------

    public void Move()
    {

        AxisX = Input.GetAxis("Horizontal");
        AxisY = Input.GetAxis("Vertical");

        moveDirection = new Vector3(AxisX, 0, AxisY).normalized;

        Walk(moveDirection);

        Fly();
    }

    public void Walk(Vector3 direction)
    {
        if (direction.magnitude >= 0.1f)
        {
            if (onGoundInstance.isOnGround)
            {
                if (stepSoundDelay <= 0) PlayStepFX();
                else stepSoundDelay -= 1;

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * playerSpeed;

                moveDir.y = this.playerRB.velocity.y;

                this.gooseAnimator.SetBool("Runnig", true);

                if (Mathf.Abs(playerRB.velocity.x) + Mathf.Abs(playerRB.velocity.z) <= 7.5f)
                {
                    this.playerRB.velocity = moveDir;
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                }
                else
                    Friction();

            }
            else if (isWingsOpen)
            {
                Vector3 moveDir = Vector3.zero;

                if (!hasWall.HasWall(this.transform))
                    moveDir = this.transform.forward;

                moveDir *= playerSpeed;

                moveDir.y = playerRB.velocity.y;

                this.transform.Rotate(new Vector3(0, AxisX * 40, 0) * Time.deltaTime);

                this.playerRB.velocity = moveDir;
            }
        }
        else
        {
            Friction();
            this.gooseAnimator.SetBool("Runnig", false);
        }
    }

    public void Jump(float jumpType)
    {

        switch (jumpType)
        {
            case 0:
                break;
            case 1:
                audioSource.PlayOneShot(audioClipJump, 0.8f);
                this.playerRB.velocity = new Vector3(playerRB.velocity.x, playerJumpForce, playerRB.velocity.z);
                //this.playerRB.AddForce(Vector3.up * playerJumpForce, ForceMode.Impulse);
                break;
            case 2:
                audioSource.PlayOneShot(audioClipJump, 0.8f);
                this.playerRB.velocity = new Vector3(playerRB.velocity.x, playerJumpForce / 1.6f, playerRB.velocity.z);
                doubleJumpCount--;
                break;
        }
    }

    public void DoRagdollEffect()
    {
        if (playerRedDoll.IsRagDoll)
        {
            this.gooseAnimator.SetBool("Runnig", false);
            this.playerRB.freezeRotation = false;
        }
        else this.playerRB.freezeRotation = true;
    }

    public void Fly()
    {
        isWingsOpen = CanOpenWings();
        this.gooseAnimator.SetBool("WingsOpen", isWingsOpen);

        if (isWingsOpen)
        {
            this.playerRB.velocity = new Vector3(playerRB.velocity.x, -playerPlaneValue, playerRB.velocity.z);
        }
    }

    public void PlayStepFX()
    {
        if ((playerRB.velocity.x * playerRB.velocity.z) != 0) {
            int randomNum = Random.Range(0, audioClipSteps.Length - 1);
            audioSource.PlayOneShot(audioClipSteps[randomNum], 0.4f - (0.3f * randomNum));
            stepSoundDelay = 16;
        }
    }

    public void PlayerDash()
    {
        playerRB.AddForce(playerRB.transform.forward * 400);
    }

    //--------------------------------TESTS----------------------------------


    public float JumpType()
    {
        if (onGoundInstance.isOnGround) return 1;
        else if (doubleJumpCount > 0) return 2;
        return 0;
    }

    public bool CanMove()
    {
        if (!playerRedDoll.IsRagDoll) //&& !playerMenu.IsMenuOn)
        {
            return true;
        }
        return false;
    }

    public bool CanOpenWings()
    {
        if (Input.GetKey(KeyCode.Space) && !onGoundInstance.isOnGround && this.playerRB.velocity.y < -playerPlaneValue + 0.01f) return true;
        return false;
    }
}


