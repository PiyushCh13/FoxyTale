using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum AnimationType
{
    Idle,
    Run,
    Jump_Up,
    Jump_Fall,
    PlayerHurt
}
public class Player_Controller : MonoBehaviour
{
    [Header("Player Components")]
    private Animator animator;
    public Rigidbody2D playerrb;

    [Space]

    [Header("Player Data")]
    public AgentDataSO agentData;

    [Space]

    [Header("Managers")]
    [System.NonSerialized] public InputManager playerInputHandler;
    [System.NonSerialized] public GroundDetector groundDetector;
    [System.NonSerialized] public AgentHealthManager healthManager;
    [System.NonSerialized] public AgentRespawnManager respawnManager;

    [Space]

    [Header("Events")]
    public UnityEvent OnRespawnRequired;

    [Header("Components")]
    public GameObject enemyBlast;
    public RawImage rawImage;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerrb = GetComponentInChildren<Rigidbody2D>();
    }

    void Start()
    {
        GameManager.Instance.currentGameStates = GameStates.isPlaying;
        InitializeAgent();
    }

    void Update()
    {
       
    }

    private void InitializeAgent()
    {
        playerInputHandler = GetComponentInChildren<InputManager>();
        //groundDetector = GetComponentInChildren<GroundDetector>();
        healthManager = GetComponentInChildren<AgentHealthManager>();
        respawnManager = GetComponentInChildren<AgentRespawnManager>();

        playerInputHandler.OnMovementVector += PlayerMove;
        agentData.jumpCount = agentData.maxJumps;
        healthManager.InitializeHealth(agentData.health);
        respawnManager.InitializeSpawnPoint(agentData.spawnPoint, transform.position);
    }

    public void PlayerMove(Vector2 input)
    {
        playerrb.linearVelocity = new Vector2(agentData.agentSpeed * input.x , playerrb.linearVelocity.y);

        if(Mathf.Abs(playerrb.linearVelocity.x) > 0.001f)
        {
            animator.Play("Player_Run");
            FaceDirection(input);
        }
    }

    // public void PlayerJump()
    // {
    //     isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);

    //     if (isGrounded)
    //     {
    //         canDoublejump = true;
    //     }

    //     if (Input.GetButtonDown("Jump"))
    //     {
    //         if (isGrounded)
    //         {
    //             playerRigidBody.linearVelocity = new Vector2(playerRigidBody.linearVelocity.x, jumpForce);
    //             AudioManager.instance.PlaySfx(10);
    //         }

    //         else
    //         {
    //             if (canDoublejump)
    //             {
    //                 playerRigidBody.linearVelocity = new Vector2(playerRigidBody.linearVelocity.x, jumpForce);
    //                 canDoublejump = false;

    //             }
    //         }
    //     }
    // }

    public void FaceDirection(Vector2 input)
    {
        if (input.x > 0)
        {
            transform.parent.localScale = new Vector3(Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y, transform.parent.localScale.z);
        }

        else if (input.x < 0)
        {
            transform.parent.localScale = new Vector3(Mathf.Abs(transform.parent.localScale.x) * -1, transform.parent.localScale.y, transform.parent.localScale.z);
        }
    }

    public void PlayerDied()
    {
        gameObject.SetActive(false);
        SceneManagement.Instance.LoadScene(rawImage, SceneList.GameOver.ToString());

    }

    public void PlayerPitFall()
    {
        OnRespawnRequired?.Invoke();
    }

    public void PlayerHit()
    {

    }

    private void PlayerCamZoom()
    {
        if (Mathf.Abs(playerrb.linearVelocity.x) > 0.1f || Mathf.Abs(playerrb.linearVelocity.y) > 0.1f)
        {
            //cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.OrthographicSize, agentData.minZoom, Time.deltaTime * 2f);
        }
        else
        {
            //cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.OrthographicSize, agentData.maxZoom, Time.deltaTime * 2f);
        }
    }
}
