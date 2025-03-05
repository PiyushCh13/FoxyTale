using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [Header("Components")]
    private Collider2D agentCollider;

    [Header("Ground Detection")]
    public LayerMask groundLayer;
    public bool isGrounded;

    [Header("Box Cast Offsets")]
    [Range(-2,2)]
    public float boxCastWidthOffset;
    public float boxCastHeightOffset;
    public float boxCastXOffset;
    public float boxCastYOffset;


    private void Awake()
    {
       if(agentCollider == null) 
        {
            agentCollider = GetComponent<Collider2D>();
        }   
    }
    
    public void CheckForGround() 
    {
        RaycastHit2D rayHit = Physics2D.BoxCast((agentCollider.bounds.center + new Vector3(boxCastXOffset,boxCastYOffset,0)), new Vector2(boxCastWidthOffset,boxCastHeightOffset), 0f, Vector2.down, 0, groundLayer);

        if(rayHit.collider != null) 
        {
            isGrounded = true;
        }
        else 
        {
            isGrounded= false;
        }
    }
}
