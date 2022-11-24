using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Controller : MonoBehaviour
{
    //---------------------------------- VARIAVEIS -----------------------------------------

    #region States

    public Player_StateMachine currentMachine;

    public Walk_PlayerState walk_PlayerState = new Walk_PlayerState();
    public Air_PlayerState air_PlayerState = new Air_PlayerState();
    public RagDoll_PlayerState ragDoll_PlayerState = new RagDoll_PlayerState();

    #endregion

    #region Audio

    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip audioClipJump;
    [SerializeField] public AudioClip[] audioClipSteps;


    [SerializeField] public float stepSoundDelay;

    #endregion

    #region Components

    [SerializeField] public Animator gooseAnimator;
    [SerializeField] public Transform cameraTransform;

    private PhotonView playerView;

    public Rigidbody playerRB;

    #endregion

    #region Scripts

    public OnGround onGoundInstance;
    public RagdollEffect playerRedDoll;
    public PlayerRespawnScrp playerRespawnScrp;

    public HasWallOnFront hasWall;

    #endregion

    #region Variaveis

    private float AxisX, AxisY;

    public float turnSmoothTime, turnSmoothVelocity, airJumpCount, dashCoolDown;

    [SerializeField] public float frictionValue, playerBaseSpeed;
    [SerializeField] public float maxAirJumpCount;

    public float playerSpeed, playerJumpForce, playerPlaneValue;

    public bool isWingsOpen;

    public Vector3 moveDirection;

    #endregion

    //--------------------------------Starting Values------------------------------------

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
        if (!playerView.IsMine) {
            DestoyExtra();
            return;
        }

        StartValues();
        currentMachine = walk_PlayerState;

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
        this.turnSmoothTime = 0.05f;
        this.stepSoundDelay = 5f;
        this.isWingsOpen = false;
        UpdateSpeed();
    }

    public void UpdateSpeed()
    {
        this.playerSpeed = this.playerBaseSpeed;
    }


    // Update is called once per frame
    void Update()
    {
        if (playerView.IsMine)
        {
            {
                Debug.Log(currentMachine);
                currentMachine.UpdateState(this);
            }
        }
    }

    //----------------------------------------- ACTIONS -----------------------------------

    public void ChangeState(Player_StateMachine newState)
    {
        this.currentMachine = newState;
        this.currentMachine.EnterState(this);
    }

    public bool IsRagdollEffect()
    {
        if (playerRedDoll.IsRagDoll)
            return true;
        return false;                }

    public void PlayStepFX()
    {
        if ((playerRB.velocity.x * playerRB.velocity.z) != 0)
        {
            int randomNum = Random.Range(0, audioClipSteps.Length - 1);
            audioSource.PlayOneShot(audioClipSteps[randomNum], 0.4f - (0.3f * randomNum));
            stepSoundDelay = 16;
        }
    }
}
