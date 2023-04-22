using UnityEngine;
public class PlayerController : MonoBehaviour
{
    
    [Header("Player Feel")]
    public float JumpForce;
    public float JumpTime;

    [Header("Ground Checks")]
    public Transform FeetPos;
    public LayerMask GroundLayer;
    public float CheckRadius;
    
    [Header("Cosmetics")]
    public bool FlipWhenFacingRight;
    
    
   [Header("Inputs")]
    private float _horizontalInput;
    private bool _jumpInputDown;
    private bool _jumpInputUp;
    private bool _jumpInputHold;
    
    [Header("Under The Hood")]
    private Rigidbody2D _rb;
    private bool _isGrounded = true;
    private bool _isJumping = false;
    private float _jumpTimeCounter;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
         _horizontalInput = Input.GetAxisRaw("Horizontal");
         _jumpInputDown = Input.GetKeyDown(KeyCode.Space);
         _jumpInputUp = Input.GetKeyUp(KeyCode.Space);
         _jumpInputHold = Input.GetKey(KeyCode.Space);
    }


    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(FeetPos.position, CheckRadius, GroundLayer);

        // Loop through each child object of the parent object
        foreach(Transform child in transform)
        {
            // Get all childed sprite renderers
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if(spriteRenderer != null)
            {
                // Flip the sprite horizontally
                if(_horizontalInput < 0)
                    spriteRenderer.flipX = !FlipWhenFacingRight;
                
                else if(_horizontalInput > 0)
                    spriteRenderer.flipX = FlipWhenFacingRight;
            }
        }


        // Simple Jump 
        if (_isGrounded == true && _jumpInputDown)
        {
            _rb.velocity = Vector2.up * JumpForce;
            _isJumping = true;
            _jumpTimeCounter = JumpTime;
        }

        // Hold Jump
        if (_jumpInputHold && _isJumping == true)
        {
            if (_jumpTimeCounter > 0)
            {
                _rb.velocity = Vector2.up * JumpForce;
                _jumpTimeCounter -= Time.deltaTime;
            }
            else
                _isJumping = false;
        }

        // Jump End
        if (_jumpInputUp)
            _isJumping = false;
    }
}
