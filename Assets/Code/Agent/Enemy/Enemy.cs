using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed;
    public Transform leftpoint, rightpoint;
    private bool movingRight;


    private Rigidbody2D theRB;
    private SpriteRenderer renderEnemySprite;
    private Animator anim;

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        renderEnemySprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();

        leftpoint.parent = transform.parent;
        rightpoint.parent = transform.parent;
        movingRight = true;
        moveCount = moveTime;

    }

    void Update()
    {
        Moving();
    }


    public void Moving()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            if (movingRight)
            {
                theRB.linearVelocity = new Vector2(enemySpeed, theRB.linearVelocity.y);

                if (transform.position.x > rightpoint.position.x)
                {
                    movingRight = false;
                    renderEnemySprite.flipX = false;
                }
            }

            else
            {
                theRB.linearVelocity = new Vector2(-enemySpeed, theRB.linearVelocity.y);

                if (transform.position.x < leftpoint.position.x)
                {
                    movingRight = true;
                    renderEnemySprite.flipX = true;
                }

            }

            if (moveCount <= 0)
            {
                waitCount = waitTime;
            }

            anim.SetBool("IsMoving", true);
        }

        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            theRB.linearVelocity = new Vector2(0, theRB.linearVelocity.y);


            if (waitCount <= 0)
            {
                moveCount = moveTime;
            }

            anim.SetBool("IsMoving", false);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerCollectibleManager>().isInvisible)
                return;
                
            other.GetComponent<PlayerCollectibleManager>().RemoveHealth();
            other.GetComponent<Player_Controller>().KnockBackForce();

            SFXManager.Instance.PlaySound(SFXManager.Instance.playerHurt);
        }

    }
}
