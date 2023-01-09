using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EpicBot_Controller : MonoBehaviour
{
    //---------------------------------- VARIAVEIS -----------------------------------------

    #region States

    public Bot_StateMachine botCurrentMachine;

    public DumbWalk_BotState dumbWalk_BotState = new DumbWalk_BotState();
    public EpicWalk_BotState epicWalk_BotState = new EpicWalk_BotState();
    public Air_BotState air_BotState = new Air_BotState();
    public RagDoll_BotState ragDoll_BotState = new RagDoll_BotState();
    public Epicbot_Reespawning respawning_BotState = new Epicbot_Reespawning();

    #endregion

    #region Audio

    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip audioClipJump;
    [SerializeField] public AudioClip[] audioClipSteps;


    [SerializeField] public float stepSoundDelay;

    #endregion

    #region Components

    [SerializeField] public Animator gooseAnimator;

    private PhotonView botView;

    public Rigidbody botRB;

    #endregion

    #region Scripts

    public OnGround onGoundInstance;
    public RagdollEffect botRedDoll;
    public PlayerRespawnScrp botRespawnScrp;

    public Epic_Path_Handle path_Handle;

    public HasWallOnFront hasWall;

    public FaseManager faseManager;

    #endregion

    #region Variaveis

    public float turnSmoothTime, turnSmoothVelocity, airJumpCount;

    [SerializeField] public float frictionValue, playerBaseSpeed;
    [SerializeField] public float maxAirJumpCount;

    public float botSpeed, botJumpForce, botPlaneValue, changeBotMoveTypeCooldown;

    public bool isWingsOpen, spaceIsPressed, isOnWater;

    public Vector3 moveDirection;

    public int vidaParaoTitanic;

    #endregion

    //--------------------------------Starting Values------------------------------------

    private void Awake()
    {
        onGoundInstance = GetComponentInChildren<OnGround>();
        hasWall = new HasWallOnFront();
        botView = GetComponent<PhotonView>();
        botRespawnScrp = GetComponent<PlayerRespawnScrp>();
        this.botRB = this.GetComponent<Rigidbody>();
        botRedDoll = this.GetComponent<RagdollEffect>();
        faseManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<FaseManager>();

    }

    private void Start()
    {
        StartValues();
        botCurrentMachine = respawning_BotState;
    }

    public void StartValues()
    {
        this.turnSmoothTime = 0.05f;
        this.stepSoundDelay = 0.5f;
        this.isWingsOpen = false;
        this.vidaParaoTitanic = 3;
        CleanSpeed();
    }

    public void CleanSpeed()
    {
        this.botSpeed = this.playerBaseSpeed;
    }

    //---------------------------------------Update---------------------------------------

    // Update is called once per frame
    void Update()
    {
        if (this.vidaParaoTitanic <= 0 && faseManager.isFaseTitanic)
        {
            faseManager.KickBot(this.gameObject);
        }

        botCurrentMachine.UpdateState(this);
    }

    //----------------------------------------- ACTIONS -----------------------------------

    public void ChangeState(Bot_StateMachine newState)
    {
        this.botCurrentMachine = newState;
        this.botCurrentMachine.EnterState(this);
    }

    public bool IsRagdollEffect()
    {
        return botRedDoll.IsRagDoll ? true : false;
    }
}
