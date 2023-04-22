using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public bool FlipWhenFacingRight;
    private Rigidbody2D _rb;
    public float JumpForce;
    public bool IsGrounded = true;
    private bool _isJumping = false;
    public Transform FeetPos;
    public float CheckRadius;
    public LayerMask GroundLayer;
    private float _jumpTimeCounter;
    public float JumpTime;
    
    

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


        // Simple Jump 
        if (IsGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = Vector2.up * JumpForce;
            _isJumping = true;
            _jumpTimeCounter = JumpTime;
        }

        // Hold Jump
        if (Input.GetKey(KeyCode.Space) && _isJumping == true)
        {
            if (_jumpTimeCounter > 0)
            {
                _rb.velocity = Vector2.up * JumpForce;
                _jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _isJumping = false;
            }
        }

        // Jump End
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isJumping = false;
        }
    }
}
