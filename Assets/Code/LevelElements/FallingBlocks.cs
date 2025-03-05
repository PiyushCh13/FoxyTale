using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlocks : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.gravityScale = 0;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GravityCoroutine());
        }
    }

    private IEnumerator GravityCoroutine() 
    {
        yield return new WaitForSeconds(1f);
        rb.isKinematic = false;
        rb.gravityScale = 3.5f;
    }
}
