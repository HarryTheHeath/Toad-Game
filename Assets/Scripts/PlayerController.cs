using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Player Feel")]

    [Range(0, 20)]
    public int JumpForce = 10;
    [Range(0, 0.6f)]
    public float JumpTime = 0.3f;

    [Header("Ground Checks")]
    public Transform FeetPos;
    public LayerMask GroundLayer;
    public float CheckRadius;
    public bool IsGrounded = true;


    [Header("Breath Attack")] 
    public float BreathHoldDuration;
    public bool Breathing;
    public Slider BreathMetre;
    public float MaxBreath;


    [Header("Cosmetics")]

    [Header("Inputs")]
    private float _horizontalInput;
    private bool _jumpInputDown;
    private bool _jumpInputUp;
    private bool _jumpInputHold;
    
    private bool _breathInput;
    private bool _breathInputHold;
    private bool _breathInputUp;

    
    [Header("Under The Hood")]
    private Rigidbody2D _rb;
    private bool _isJumping = false;
    private float _jumpTimeCounter;

    private void Awake() => GetPlayerComponents();

    private void Start()
    {
        BreathMetre.maxValue = MaxBreath;
    }
    
    

    private void FixedUpdate() {}


    private void Update()
    {
        CheckInputs();
        FlipPlayer();
        CalculateJump();
        CalculateBreath();
    }

    
    
    
    private void GetPlayerComponents()=> _rb = GetComponent<Rigidbody2D>();
    
    
    
    private void CheckInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _jumpInputDown = Input.GetKeyDown(KeyCode.Space);
        _jumpInputUp = Input.GetKeyUp(KeyCode.Space);
        _jumpInputHold = Input.GetKey(KeyCode.Space);
        
        _breathInputHold = Input.GetMouseButton(0);
        _breathInputUp = Input.GetMouseButtonUp(0);
    }

    
    
    
    
    private void FlipPlayer()
    {
        if (_horizontalInput < 0) 
            transform.eulerAngles = Vector3.zero;

        else if (_horizontalInput > 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
    }

    
    
    
    
    private void CalculateJump()
    {
        IsGrounded = Physics2D.OverlapCircle(FeetPos.position, CheckRadius, GroundLayer);

        // Simple Jump 
        if (IsGrounded && _jumpInputDown)
        {
           _jumpTimeCounter = JumpTime; 
           _rb.velocity = Vector2.up * JumpForce;
           _isJumping = true;
        }

        // Hold Jump
        if (_jumpInputHold && _isJumping)
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



    private void CalculateBreath()
    {
        
        if (_breathInputHold)
        {
            Breathing = true;
            BreathHoldDuration += Time.deltaTime;
            Debug.Log("Breathing In");
        }

        if (_breathInputUp)
        {
            Breathing = false;
            Debug.Log("Held breath for: " + BreathHoldDuration + " seconds.");
            BreathHoldDuration = 0f;
        }
        
        BreathMetre.value = BreathHoldDuration;
    }
}
