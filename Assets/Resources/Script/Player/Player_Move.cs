using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Move : MonoBehaviour
{

    //---------------------------------- VARIAVEIS -----------------------------------------
    private OnGround onGoundInstance;
    private Player_RagdollEffect playerRedDoll;
    private PlayerMEnu playerMenu;

    private float AxisX, AxisY;

    private Rigidbody playerRB;

    [SerializeField] private Animator wingAnimator;
    [SerializeField] private Transform cam;
    [SerializeField] private float frictionValue, playerBaseSpeed, stepSoundDelay;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipJump;
    [SerializeField] private AudioClip[] audioClipSteps;

    [SerializeField] private float maxDoubleJumpCount;

    private bool isWingsOpen;

    public float playerSpeed, playerJumpForce, playerPlaneValue;

    private float turnSmoothTime, turnSmoothVelocity, doubleJumpCount;

    private Vector3 playerActualSpeed;

    private PhotonView playerView;

    private void Awake()
    {
        onGoundInstance = GetComponentInChildren<OnGround>();
        playerView = GetComponent<PhotonView>();
        playerMenu = GetComponent<PlayerMEnu>();
        this.playerRB = this.GetComponent<Rigidbody>();
        playerRedDoll = this.GetComponent<Player_RagdollEffect>();
    }

    private void Start()
    {
        if (!playerView.IsMine)
            DestoyExtra();
        else
            StartValues();
    }

    public void DestoyExtra()
    {
        Destroy(GetComponentInChildren<Camera>().gameObject);
        Destroy(GetComponentInChildren<Cinemachine.CinemachineCollider>().gameObject);
        Destroy(playerRB);
        Destroy(onGoundInstance);
        Destroy(playerRedDoll);
        Destroy(playerMenu);
    }

    public void StartValues()
    {
        this.turnSmoothTime = 0.1f;
        this.stepSoundDelay = 5f;
        this.isWingsOpen = false;
        UpdateSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerView.IsMine)
        {
            UpdateValues();

            if (Input.GetKeyDown(KeyCode.Space) && !playerRedDoll.IsRagDoll) Jump(JumpType());

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
        playerActualSpeed = playerRB.velocity;
        if (onGoundInstance.isOnGround && doubleJumpCount != maxDoubleJumpCount) doubleJumpCount = maxDoubleJumpCount;
    }

    public void UpdateSpeed()
    {
        this.playerSpeed = this.playerBaseSpeed;
    }

    public void Friction()
    {
        if (Mathf.Abs(playerActualSpeed.x) > 1)
            playerActualSpeed.x -= Mathf.Sign(playerActualSpeed.x) * frictionValue * Time.deltaTime;
        else
            playerActualSpeed.x = 0;

        if (Mathf.Abs(playerActualSpeed.z) > 1)
            playerActualSpeed.z -= Mathf.Sign(playerActualSpeed.z) * frictionValue * Time.deltaTime;
        else
            playerActualSpeed.z = 0;
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
            if (onGoundInstance.isOnGround)
            {
                if (stepSoundDelay <= 0) PlayStepFX();
                else stepSoundDelay -= 1;

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * playerSpeed;

                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                moveDir.y = this.playerRB.velocity.y;

                this.playerRB.velocity = moveDir;
                //if (Mathf.Abs(playerActualSpeed.x) + Mathf.Abs(playerActualSpeed.z) < 5)
                //    this.playerRB.velocity += moveDir * Time.deltaTime;
            }
            else if (isWingsOpen)
            {
                Vector3 moveDir = this.transform.forward;

                moveDir *= playerSpeed;

                moveDir.y = playerRB.velocity.y;

                this.transform.Rotate(new Vector3(0, AxisX * 40, 0) * Time.deltaTime);

                this.playerRB.velocity = moveDir;
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
                Debug.Log("B");
                break;
            case 1:
                audioSource.PlayOneShot(audioClipJump, 0.8f);
                Debug.Log("A");
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

    public void PlayStepFX()
    {
        if ((playerActualSpeed.x * playerActualSpeed.z) != 0) {
            int randomNum = Random.Range(0, audioClipSteps.Length - 1);
            audioSource.PlayOneShot(audioClipSteps[randomNum], 0.4f - (0.3f * randomNum));
            stepSoundDelay = 16; 
        }

    }

    public void ApplyForceIn(Vector3 force)
    {
        if (this.playerRedDoll.IsRagDoll)
        {
            playerRB.AddForce(force);
        }
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


