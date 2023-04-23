using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Entity
{

    public class PlayerController : MonoBehaviour
    {

        [Header("Player Feel")] [Range(0, 20)] public int JumpForce = 10;
        [Range(0, 0.6f)] public float JumpTime = 0.3f;
        [Range(0.01f, 0.2f)]public float BreathActiveTime;

        [Header("Ground Checks")] public Transform FeetPos;
        public LayerMask GroundLayer;
        public float CheckRadius;
        public bool IsGrounded = true;


        [Header("Breath Attack")] public GameObject Fus;
        public GameObject Ro;
        public GameObject Dah;
        
        [Space(20)]public float BreathHoldDuration;
        public bool Breathing;
        public Slider BreathMetre;
        public Image BreathMetreImage;
        public TextMeshProUGUI FusRoDah;

        [Space(20)] public float FusTime = 0.5f;
        public float RoTime = 1.25f;
       public float DahTime = 2f;
        public float MaxBreath = 2.25f;

        private bool CanFus = false;
        private bool CanRo = false;
        private bool CanDah = false;


        [Header("Cosmetics")] public Color DefaultColour;
        public Color FusColour;
        public Color RoColour;
        public Color DahColour;

        [Header("Audio")] public AudioClip JumpSFX;
        public AudioClip LandSFX;
        public AudioClip TakeADeepBreathSFX;
        public AudioClip FusSFX;
        public AudioClip RoSFX;
        public AudioClip DahSFX;
        public AudioSource PhysicsAudio;
        public AudioSource BreatheAudio;

        [Header("Inputs")] private float _horizontalInput;
        private bool _jumpInputDown;
        private bool _jumpInputUp;
        private bool _jumpInputHold;

        private bool _breathInput;
        private bool _breathInputHold;
        private bool _breathInputUp;


        [Header("Under The Hood")] private Rigidbody2D _rb;
        private bool _isJumping = false;
        private float _jumpTimeCounter;
        private bool _hasLanded = true;

        private void Awake() => GetPlayerComponents();

        private void Start()
        {
            BreathMetre.maxValue = MaxBreath;
            DefaultColour.a = 1;
            FusColour.a = 1;
            RoColour.a = 1;
            DahColour.a = 1;

            FusRoDah.text = "";
        }



        private void FixedUpdate()
        {
        }


        private void Update()
        {
            CheckInputs();
            FlipPlayer();
            CalculateJump();
            CalculateBreath();
            DisplayBreath();
        }


        private void GetPlayerComponents() => _rb = GetComponent<Rigidbody2D>();



        private void CheckInputs()
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _jumpInputDown = Input.GetKeyDown(KeyCode.Space);
            _jumpInputUp = Input.GetKeyUp(KeyCode.Space);
            _jumpInputHold = Input.GetKey(KeyCode.Space);

            _breathInput = Input.GetMouseButtonDown(0);
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

                _hasLanded = false;
                PhysicsAudio.clip = JumpSFX;
                PhysicsAudio.Play();
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

            if (IsGrounded && !_hasLanded)
            {
                _hasLanded = true;
                PhysicsAudio.clip = LandSFX;
                PhysicsAudio.Play();
            }
        }



        private void CalculateBreath()
        {

            if (!IsGrounded)
            {
                BreathHoldDuration = 0;
                Breathing = false;
                return;
            }

            if (_breathInput)
            {
                if (!Breathing)
                {
                    BreatheAudio.clip = TakeADeepBreathSFX;
                    BreatheAudio.Play();
                }

                Breathing = true;
            }

            if (_breathInputHold && Breathing)
            {
                BreathHoldDuration += Time.deltaTime;
                //Debug.Log("Breathing In");
            }

            if (_breathInputUp)
            {
                Breathing = false;
                //Debug.Log("Held breath for: " + BreathHoldDuration + " seconds.");
                BreathHoldDuration = 0;
                FusRoDah.text = "";

                if (CanFus)
                    Attack(Fus, FusSFX, BreathActiveTime, CanFus);

                else if (CanRo)
                    Attack(Ro, RoSFX, BreathActiveTime, CanRo);
                
                else if (CanDah)
                    Attack(Dah, DahSFX, BreathActiveTime, CanDah);
            }

            BreathMetre.value = BreathHoldDuration;
        }



        private void DisplayBreath()
        {
            if (!Breathing || !IsGrounded)
            {
                ClearBreathUI();
                return;
            }


            if (BreathHoldDuration < FusTime)
            {
                BreathMetreImage.color = DefaultColour;
                FusRoDah.text = "";
            }

            else if (BreathHoldDuration < RoTime && BreathHoldDuration > FusTime)
            {
                BreathMetreImage.color = FusColour;
                FusRoDah.text = "FUS!";
                CanFus = true;

                BreatheAudio.clip = FusSFX;
                BreatheAudio.Play();
            }

            else if (BreathHoldDuration < DahTime && BreathHoldDuration > RoTime)
            {
                BreathMetreImage.color = RoColour;
                FusRoDah.text = "RO!";
                CanFus = false;
                CanRo = true;
            }

            else if (BreathHoldDuration < MaxBreath && BreathHoldDuration > DahTime)
            {
                BreathMetreImage.color = DahColour;
                FusRoDah.text = "DAH!";
                CanRo = false;
                CanDah = true;
            }

            else if (BreathHoldDuration > MaxBreath)
                ClearBreathUI();

            FusRoDah.color = BreathMetreImage.color;
        }



        private void ClearBreathUI()
        {
            FusRoDah.text = "";
            BreathMetre.value = 0;
            CanFus = false;
            CanRo = false;
            CanDah = false;
        }
        

        private void Attack(GameObject breath, AudioClip breathSFX, float breathActiveTime, bool breathToggle)
        {
            breath.SetActive(true);
            BreatheAudio.clip = breathSFX;
            BreatheAudio.Play();
            StartCoroutine(EndBreath(breath, breathActiveTime,breathToggle));
        }
        
        
        
        
        private IEnumerator EndBreath(GameObject breath, float breathActiveTime, bool breathToggle)
        {
            breathToggle = false;
            yield return new WaitForSeconds(breathActiveTime);
            OnEndBreath(breath);
        }

        
        

        private static void OnEndBreath(GameObject breath) => breath.SetActive(false);
    }
}
