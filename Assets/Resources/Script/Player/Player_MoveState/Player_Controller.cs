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
    public Swim_PlayerState swim_PlayerState = new Swim_PlayerState();
    public Air_PlayerState air_PlayerState = new Air_PlayerState();
    public RagDoll_PlayerState ragDoll_PlayerState = new RagDoll_PlayerState();
    public Spawning_PlayerState spawning_PlayerState = new Spawning_PlayerState();

    #endregion

    #region Audio

    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip audioClipJump;
    [SerializeField] public AudioClip audioClipDoubleJump;
    [SerializeField] public AudioClip audioClipDash;

    [SerializeField] public AudioClip[] audioClipSteps;


    [SerializeField] public float stepSoundDelay;

    #endregion

    #region Components

    [SerializeField] public Animator gooseAnimator;
    [SerializeField] public Transform cameraTransform;

    [SerializeField] public PhotonView playerView;

    public Rigidbody playerRB;

    #endregion

    #region Scripts

    public OnGround onGoundInstance;
    public RagdollEffect playerRedDoll;
    public PlayerRespawnScrp playerRespawnScrp;

    public HasWallOnFront hasWall;

    public FaseManager faseManager;

    #endregion

    #region Variaveis

    private float AxisX, AxisY;

    public float turnSmoothTime, turnSmoothVelocity, airJumpCount, baseDashCoolDown, gravityValue;

    [SerializeField] public float frictionValue, playerBaseSpeed;
    [SerializeField] public float maxAirJumpCount;

    public float playerSpeed, playerJumpForce, playerPlaneValue;

    public bool isWingsOpen, isOnWater;

    public Vector3 moveDirection;

    [SerializeField] public Transform playerParentTransform;

    public int vidaParaoTitanic;

    #endregion

    //--------------------------------Starting Values------------------------------------

    private void Awake()
    {
        onGoundInstance = GetComponentInChildren<OnGround>();
        hasWall = new HasWallOnFront();
        playerRespawnScrp = GetComponent<PlayerRespawnScrp>();
        this.playerRB = this.GetComponent<Rigidbody>();
        playerRedDoll = this.GetComponent<RagdollEffect>();
        //faseManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<FaseManager>();
    }

    private void Start()
    {
        if (!playerView.IsMine) {
            DestoyExtra();
            return;
        }

        StartValues();
        UpdateSpeed();

        this.ChangeState(spawning_PlayerState);

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
        this.baseDashCoolDown = 1.5f;
        this.maxAirJumpCount = 0;
        this.gravityValue = 0;
        vidaParaoTitanic = 3;

        int a = 0;

        switch (a)
        {
            case 0:
                this.baseDashCoolDown -= 0.75f;
                Debug.Log("Mais dash");
                break;
            case 1:
                this.maxAirJumpCount += 1;
                Debug.Log("Mais Pulo");
                break;
            case 2:
                this.gravityValue += 1;
                Debug.Log("Mais gravidade");
                break;
            case 3:
                this.gravityValue -= 2;
                Debug.Log("Menos gravidade");
                break;

        }
    }

    public void UpdateSpeed()
    {
        this.playerSpeed = this.playerBaseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            faseManager.LeveRoom();
        }

        if(vidaParaoTitanic <= 0 && faseManager.isFaseTitanic)
        {
            faseManager.LeveRoom();
        }


        currentMachine.UpdateState(this);
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
        return false;
    }

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
