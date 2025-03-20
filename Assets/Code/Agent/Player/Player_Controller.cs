using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using System.Collections;
public class Player_Controller : MonoBehaviour
{
    [Header("Player Components")]
    private Animator player_Anim;
    private Rigidbody2D player_RB;
    private SpriteRenderer player_SR;
    private BoxCollider2D player_Collider;

    [Space]

    [Header("Managers")]
    [System.NonSerialized] public PlayerCollectibleManager collectibleManager;
    [System.NonSerialized] public PlayerRespawnManager respawnManager;

    [Space]

    [Header("External Objects")]
    [SerializeField] private CinemachineCamera cinemachineCamera;
    public RawImage rawImage;
    [SerializeField] private GameObject dustParticles;

    [Header("Camera Controls")]
    public float minZoom;
    public float maxZoom;

    [Header("Jump Data")]
    [Space]
    public bool isGrounded;
    public bool isJumpPressed;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public float jumpForce = 12f;
    public float maxJumpTime = 0.25f;
    public float jumpMultiplier = 30f;
    public float gravityMultiplier = 35f;
    public float maxFallSpeed = 18f;
    private float jumpTimeCounter;
    private bool isJumping;

    [Space]

    [Header("Player Data")]
    public float speed;
    private Vector2 movementVector;

    [Header("Player Sounds")]
    public AudioClip jumpSound;

    [Header("Knockback Settings")]
    public float knockbackForce = 10f;
    public float knockbackLength;
    private float knockbackCounter;

    private bool stopInput;



    private void Awake()
    {

    }

    void Start()
    {
        GameManager.Instance.currentGameStates = GameStates.isPlaying;
        MusicManager.Instance.PlayMusic(MusicManager.Instance.mainLevel);
        InitialisePlayer();
    }

    void Update()
    {
        if (knockbackCounter <= 0)
        {
            CheckForGround();
            DustParticles();
            PlayJumpAnimation();
        }
        else
        {
            knockbackCounter -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (knockbackCounter <= 0)
        {
            PlayerMove();
            HandleJump();
        }
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
        player_Collider = GetComponent<BoxCollider2D>();

        collectibleManager = GetComponent<PlayerCollectibleManager>();
        respawnManager = GetComponent<PlayerRespawnManager>();
        collectibleManager.InitializeHealth();
    }

    public void HandleMovement(InputAction.CallbackContext context)
    {
        movementVector = new Vector2(context.ReadValue<Vector2>().x, 0);
    }

    public void PlayerMove()
    {
        player_RB.linearVelocity = new Vector2(speed * movementVector.x, player_RB.linearVelocity.y);
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
            SFXManager.Instance.PlaySound(jumpSound);
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

    public void PlayerDied()
    {
        gameObject.SetActive(false);
        SceneManagement.Instance.LoadScene(rawImage, SceneList.GameOver.ToString());

    }

    public void PlayerPitFall()
    {
        StartCoroutine(SceneManagement.Instance.FadeToBlackUI(rawImage));
        respawnManager.RespawnPlayer(this);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pitfall"))
        {
            collectibleManager.RemoveHealth();
            PlayerPitFall();
        }
    }

    public void KnockBackForce()
    {
        knockbackCounter = knockbackLength;
        KnockBackForceDirection();
    }

    public void KnockBackForceDirection()
    {
        if (player_SR.flipX)
        {
            player_RB.linearVelocity = new Vector2(knockbackForce, knockbackForce);
        }

        else
        {
            player_RB.linearVelocity = new Vector2(-knockbackForce, knockbackForce);
        }
    }
}
