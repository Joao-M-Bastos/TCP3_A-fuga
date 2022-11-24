using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bot_Move : MonoBehaviour
{

    //---------------------------------- VARIAVEIS -----------------------------------------
    private OnGround onGoundInstance;
    private RagdollEffect botRedDoll;

    private Rigidbody botRB;

    private HasWallOnFront hasWall;

    [SerializeField] private Animator wingAnimator;
    [SerializeField] private float frictionValue, botBaseSpeed, stepSoundDelay;

    [SerializeField] private AudioSource audioSource;
    //[SerializeField] private AudioClip audioClipJump;
    //[SerializeField] private AudioClip[] audioClipSteps;

    [SerializeField] private float maxDoubleJumpCount;

    private bool isWingsOpen, spaceIsPressed;

    public float botSpeed, botJumpForce, botPlaneValue;

    private float turnSmoothTime, turnSmoothVelocity, doubleJumpCount, changeDirectionDelay;
    private float randx, randy;

    private Vector3 botActualSpeed;

    private PhotonView playerView;

    private void Awake()
    {
        hasWall = new HasWallOnFront();
        randx = 1;
        randy = 1;
        onGoundInstance = GetComponentInChildren<OnGround>();
        playerView = GetComponent<PhotonView>();
        this.botRB = this.GetComponent<Rigidbody>();
        botRedDoll = this.GetComponent<RagdollEffect>();
    }

    private void Start()
    {
        //if (!playerView.IsMine)
            //DestoyExtra();
        //else
            StartValues();
    }

    public void DestoyExtra()
    {
        Destroy(GetComponentInChildren<Camera>().gameObject);
        Destroy(GetComponentInChildren<Cinemachine.CinemachineCollider>().gameObject);
        Destroy(botRB);
        Destroy(onGoundInstance);
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
        //if (playerView.IsMine)
        //{
            UpdateValues();

            BotJump();

            DoRagdollEffect();
        //}
    }

    private void FixedUpdate()
    {
        //if (playerView.IsMine)
        //{
            if (CanMove()) Move();
        //}
    }

    //--------------------------------- UPDATE INSTANCES --------------------------------

    public void UpdateValues()
    {
        botActualSpeed = botRB.velocity;
        if (onGoundInstance.isOnGround && doubleJumpCount != maxDoubleJumpCount) doubleJumpCount = maxDoubleJumpCount;
    }

    public void UpdateSpeed()
    {
        this.botSpeed = this.botBaseSpeed;
    }

    public void Friction()
    {
        if (Mathf.Abs(botActualSpeed.x) > 1)
            botActualSpeed.x -= Mathf.Sign(botActualSpeed.x) * frictionValue * Time.deltaTime;
        else
            botActualSpeed.x = 0;

        if (Mathf.Abs(botActualSpeed.z) > 1)
            botActualSpeed.z -= Mathf.Sign(botActualSpeed.z) * frictionValue * Time.deltaTime;
        else
            botActualSpeed.z = 0;
        this.botRB.velocity = botActualSpeed;
    }

    //----------------------------------------- ACTIONS -----------------------------------

    public void Move()
    {
        Vector3 direction;
        

        if (changeDirectionDelay > 60)
        {
            this.randx = Mathf.Floor(Random.Range(-1, 2));
            this.randy = Mathf.Floor(Random.Range(-1, 2));
            changeDirectionDelay = 0;
        }
        else
        {
            changeDirectionDelay++;
            //changeDirectionDelay += Time.deltaTime * Random.Range(0, 5);
        }

        if (hasWall.HasWall(this.transform))
        {
            randx *= -1;
            randy *= -1;
        }

        direction = new Vector3(randx, 0, randy).normalized;

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

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * botSpeed;

                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                moveDir.y = this.botRB.velocity.y;

                this.botRB.velocity = moveDir;
                //if (Mathf.Abs(playerActualSpeed.x) + Mathf.Abs(playerActualSpeed.z) < 5)
                //    this.playerRB.velocity += moveDir * Time.deltaTime;
            }
            else if (isWingsOpen)
            {
                Vector3 moveDir = Vector3.zero;

                if (!hasWall.HasWall(this.transform))
                    moveDir = this.transform.forward;
                

                moveDir *= botSpeed;

                moveDir.y = botRB.velocity.y;

                this.transform.Rotate(new Vector3(0, randx * 40, 0) * Time.deltaTime);

                this.botRB.velocity = moveDir;
            }
        }
        else
        {
            Friction();
        }
    }

    public void BotJump()
    {
        if (spaceIsPressed)
        {
            if (!botRedDoll.IsRagDoll)
            {
                //audioSource.PlayOneShot(audioClipJump, 0.8f);
                this.botRB.velocity = new Vector3(botRB.velocity.x, botJumpForce, botRB.velocity.z);
            }

            if (onGoundInstance.isOnGround && botActualSpeed.y < 4) spaceIsPressed = false;
        }
        else
        {
            if (Random.Range(0, 100) > 99)
                spaceIsPressed = false;
        }
    }

    public void DoRagdollEffect()
    {
        if (botRedDoll.IsRagDoll)
        {
            this.botRB.freezeRotation = false;
        }
        else this.botRB.freezeRotation = true;
    }

    public void Fly()
    {
        isWingsOpen = CanOpenWings();
        this.wingAnimator.SetBool("OpenWings", isWingsOpen);

        if (isWingsOpen)
        {
            this.botRB.velocity = new Vector3(botRB.velocity.x, -botPlaneValue, botRB.velocity.z);
        }
    }

    public void PlayStepFX()
    {
        /*
        if ((botActualSpeed.x * botActualSpeed.z) != 0)
        {
            int randomNum = Random.Range(0, audioClipSteps.Length - 1);
            audioSource.PlayOneShot(audioClipSteps[randomNum], 0.4f - (0.3f * randomNum));
            stepSoundDelay = 16;
        }
        */
    }

    public void ApplyForceIn(Vector3 force)
    {
        if (this.botRedDoll.IsRagDoll)
        {
            botRB.AddForce(force);
        }
    }

    //--------------------------------TESTS----------------------------------


    public bool CanMove()
    {
        if (!botRedDoll.IsRagDoll) //&& !playerMenu.IsMenuOn)
        {
            return true;
        }
        return false;
    }

    public bool CanOpenWings()
    {
        if (spaceIsPressed && !onGoundInstance.isOnGround && this.botRB.velocity.y < -botPlaneValue + 0.01f) return true;
        return false;
    }
}


