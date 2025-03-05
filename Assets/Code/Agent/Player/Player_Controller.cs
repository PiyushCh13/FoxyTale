using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
public class Player_Controller : MonoBehaviour
{
    [Header("Player Components")]
    private Animator player_Anim;
    private Rigidbody2D player_RB;
    private SpriteRenderer player_SR;

    [Space]

    [Header("Player Data")]
    public PlayerDataSO playerData;

    [Space]

    [Header("Managers")]
    [System.NonSerialized] public AgentHealthManager healthManager;
    [System.NonSerialized] public AgentRespawnManager respawnManager;

    [Space]

    [Header("Events")]
    public UnityEvent OnRespawnRequired;

    [Header("External Objects")]

    [SerializeField] private CinemachineCamera cinemachineCamera;
    public GameObject enemyBlast;
    public RawImage rawImage;

    [Header("Camera Controls")]
    public float minZoom;
    public float maxZoom;

    [Header("Movement Data")]
    [Space]
    public float speed;
    [SerializeField] private GameObject dustParticles;


    [Header("Jump Data")]
    [Space]
    public bool isGrounded;
    public bool isJumpPressed;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public float jumpForce = 12f;          // Initial jump force
    public float maxJumpTime = 0.25f;      // Maximum time to hold jump
    public float jumpMultiplier = 30f;     // Extra force while holding jump
    public float gravityMultiplier = 35f;  // Gravity increase for fast falling
    public float maxFallSpeed = 18f;       // Limits max falling speed
    private float jumpTimeCounter;
    private bool isJumping;


    private void Awake()
    {

    }

    void Start()
    {
        GameManager.Instance.currentGameStates = GameStates.isPlaying;
        InitialisePlayer();
    }

    void Update()
    {
        CheckForGround();
        DustParticles();
        PlayJumpAnimation();
    }

    void FixedUpdate()
    {
        HandleJump();
    }

    void LateUpdate()
    {
        PlayerCamZoom();
    }

    void InitialisePlayer()
    {
        player_Anim = GetComponent<Animator>();
        player_RB = GetComponent<Rigidbody2D>();
        player_SR = GetComponent<SpriteRenderer>();

        healthManager = GetComponent<AgentHealthManager>();
        respawnManager = GetComponent<AgentRespawnManager>();
        //healthManager.InitializeHealth(playerData.health);
        //respawnManager.InitializeSpawnPoint(playerData.spawnPoint, transform.position);
    }

    public void HandleMovement(InputAction.CallbackContext context)
    {
        player_RB.linearVelocity = new Vector2(speed * context.ReadValue<Vector2>().x, player_RB.linearVelocity.y);
        ChangePlayerSpriteDirection(player_RB.linearVelocity);
        player_Anim.SetFloat("MOVE_SPEED", Mathf.Abs(player_RB.linearVelocity.x));
    }

    public void CheckForJump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
            player_RB.linearVelocity = new Vector2(player_RB.linearVelocity.x, jumpForce);
        }
        else if (context.canceled)
        {
            isJumping = false;
        }
    }

    void HandleJump()
    {
        if (isJumping && jumpTimeCounter > 0)
        {
            player_RB.linearVelocity += Vector2.up * (jumpMultiplier * Time.fixedDeltaTime);
            jumpTimeCounter -= Time.fixedDeltaTime;
        }
        else
        {
            ApplyGravity();
        }
    }

    void ApplyGravity()
    {
        player_RB.linearVelocity += Vector2.down * (gravityMultiplier * Time.fixedDeltaTime);

        if (player_RB.linearVelocity.y < -maxFallSpeed)
        {
            player_RB.linearVelocity = new Vector2(player_RB.linearVelocity.x, -maxFallSpeed);
        }
    }

    void ChangePlayerSpriteDirection(Vector2 input)
    {
        if (input.x < 0)
        {
            player_SR.flipX = true;
        }

        else if (input.x > 0)
        {
            player_SR.flipX = false;
        }
    }

    void CheckForGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);
    }

    void DustParticles()
    {
        if (isGrounded)
        {
            dustParticles.SetActive(true);
        }
        else
        {
            dustParticles.SetActive(false);
        }
    }

    void PlayJumpAnimation()
    {
        if (!isGrounded)
        {
            if (player_RB.linearVelocity.y > 0)
            {
                player_Anim.Play("Player_Jump_Up");
            }
            else if (player_RB.linearVelocity.y < 0)
            {
                player_Anim.Play("Player_Jump_Fall");
            }
        }

        else
        {
            if (Mathf.Abs(player_RB.linearVelocity.x) > 0.1f)
            {
                player_Anim.Play("Player_Run");
            }
            else
            {
                player_Anim.Play("Player_Idle");
            }
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
        if (Mathf.Abs(player_RB.linearVelocity.x) > 0.1f || Mathf.Abs(player_RB.linearVelocity.y) > 0.1f)
        {
            cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, maxZoom, Time.deltaTime * 2f);
        }
        else
        {
            cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, minZoom, Time.deltaTime * 2f);
        }
    }
}
