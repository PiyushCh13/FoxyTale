using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public enum BossState { Shooting, Hurt, Moving, Ended };
    public BossState currentState;

    [Header("Components")]
    public Transform bossTransform;
    public Animator anim;

    public Slider healthBar;

    [Header("Movement")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;

    public bool isMoving = false;
    private bool moveRight;

    [Header("Mine Dropping")]
    public GameObject mine;
    public Transform minePoint;
    public float timeBetweenMines;
    private float mineCounter;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float timeBetweenShots;
    private float shotCounter;
    public Transform firePoint;

    [Header("Hurt State")]
    public float hurtDuration;
    private float hurtCounter;
    public GameObject hitBox;

    [Header("Health")]
    public int health;
    public GameObject explosionPrefab;
    private bool isDefeated;
    public float shotSpeedMultiplier;
    public float mineSpeedMultiplier;
    public BoxCollider2D invisibleWall;

    [Header("Sounds")]
    public AudioClip bossDeathSound;
    public AudioClip bossImpactSound;

    private void Start()
    {
        healthBar.gameObject.SetActive(true);
        healthBar.maxValue = health;
        currentState = BossState.Shooting;
        invisibleWall.isTrigger = false;
        ResetTimers();

        MusicManager.Instance.PlayMusic(MusicManager.Instance.bossBattle);
    }

    private void Update()
    {
        if (currentState == BossState.Ended) return;

        StateMachine();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H)) TakeHit();
#endif
    }

    private void StateMachine()
    {
        switch (currentState)
        {
            case BossState.Shooting:
                HandleShooting();
                break;

            case BossState.Hurt:
                HandleHurt();
                break;

            case BossState.Moving:
                HandleMovement();
                break;
        }
    }

    private void HandleShooting()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0)
        {
            shotCounter = timeBetweenShots;
            GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            newBullet.transform.localScale = bossTransform.localScale * newBullet.transform.localScale.x;
        }
    }

    private void HandleHurt()
    {
        if (hurtCounter > 0)
        {
            hurtCounter -= Time.deltaTime;

            if (hurtCounter <= 0)
            {
                currentState = isDefeated ? BossState.Ended : BossState.Moving;
                mineCounter = 0;
            }
        }
    }

    private void HandleMovement()
    {
        MoveBoss();
        DropMines();
    }

    private void MoveBoss()
    {
        float moveDirection = moveRight ? 1f : -1f;
        bossTransform.position += new Vector3(moveSpeed * Time.deltaTime * moveDirection, 0f, 0f);

        if (moveRight && bossTransform.position.x > rightPoint.position.x)
        {
            moveRight = false;
            bossTransform.localScale = new Vector3(Mathf.Abs(bossTransform.localScale.x), bossTransform.localScale.y, bossTransform.localScale.z);
            StopMoving();
        }
        else if (!moveRight && bossTransform.position.x < leftPoint.position.x)
        {
            moveRight = true;
            bossTransform.localScale = new Vector3(bossTransform.localScale.x * -1f, bossTransform.localScale.y, bossTransform.localScale.z);
            StopMoving();
        }
    }

    private void DropMines()
    {
        mineCounter -= Time.deltaTime;

        if (mineCounter <= 0)
        {
            mineCounter = timeBetweenMines;
            Instantiate(mine, minePoint.position, minePoint.rotation);
        }
    }

    public void TakeHit()
    {
        currentState = BossState.Hurt;
        hurtCounter = hurtDuration;
        anim.SetTrigger("Hit");
        SFXManager.Instance.PlaySound(bossImpactSound);

        DestroyAllMines();

        health -= 1;
        healthBar.value = health;

        if (health <= 0)
        {
            Die();

        }
        else
        {
            SpeedUpAttack();
        }
    }

    private void DestroyAllMines()
    {
        foreach (var foundMine in FindObjectsByType<BossTankMine>(FindObjectsSortMode.None))
        {
            foundMine.Explode();
        }
    }

    private void SpeedUpAttack()
    {
        timeBetweenShots /= shotSpeedMultiplier;
        timeBetweenMines /= mineSpeedMultiplier;
    }

    private void StopMoving()
    {
        currentState = BossState.Shooting;
        shotCounter = 0f;
        anim.SetTrigger("StopMoving");
        hitBox.SetActive(true);
    }

    private void Die()
    {
        isDefeated = true;
        invisibleWall.isTrigger = true;
        SFXManager.Instance.PlaySound(bossDeathSound);
        MusicManager.Instance.StopMusic();
        MusicManager.Instance.PlayMusic(MusicManager.Instance.mainLevel);
        Instantiate(explosionPrefab, bossTransform.position, bossTransform.rotation);
        healthBar.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void ResetTimers()
    {
        shotCounter = timeBetweenShots;
        mineCounter = timeBetweenMines;
    }
}
