using System;
using UnityEngine;
public class Player : MonoBehaviour
{
    public bool FlipWhenFacingRight;
    private Rigidbody2D _rb;
    public bool IsGrounded = true;
    public Transform FeetPos;
    public float CheckRadius;
    public LayerMask GroundLayer;
    
    

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
    }


    private void Update()
    {
        // Get the horizontal input value
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        IsGrounded = Physics2D.OverlapCircle(FeetPos.position, CheckRadius, GroundLayer);

        // Loop through each child object of the parent object
        foreach(Transform child in transform)
        {
            // Get all childed sprite renderers
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if(spriteRenderer != null)
            {
                // Flip the sprite horizontally
                if(horizontalInput < 0)
                    spriteRenderer.flipX = !FlipWhenFacingRight;
                
                else if(horizontalInput > 0)
                    spriteRenderer.flipX = FlipWhenFacingRight;
            }
        }
    }
}
