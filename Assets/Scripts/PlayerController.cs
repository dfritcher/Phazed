using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        #region Fields, Properties

        public bool IsBoosting;
        public float BoostTime = 2.5f;
        public float CurrentBoostTime;
        public float Speed;
        public float Jumpforce;
        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 2.0f;
        public Transform groundCheck;
        public float CheckRadius;
        public LayerMask WhatIsGround;
        public ParticleSystem Death_Effect_White;
        public ParticleSystem Death_Effect_Black;
        public Sprite Phased_White;
        public Sprite WhiteEyes;
        public Sprite WhiteEyesBlink;
        
        public Sprite Phased_Black;
        public Sprite BlackEyes;
        public Sprite BlackEyesBlink;

        public Vector3 EyeLookingUpLocation;

        public AudioSource _jumpSound;
        public AudioSource _landSound;

        [SerializeField]
        private Camera2DFollow _followCamera;
        [SerializeField]
        private SpriteRenderer _playerSpriteRenderer;
        [SerializeField]
        private SpriteRenderer _eyesSpriteRenderer;

        private float _moveInput;
        private Rigidbody2D _rb;
        private bool _facingRight = true;
        private bool _isGrounded;
       
        private bool _pressing;

        public Phase CurrentPhase
        {
            get { return _currentPhase; }
        }
        private Phase _currentPhase = Phase.White;
        
        private bool _levelStarted = false;
        private const float BONUS_GRAV = 9.8f;
        private bool _isInitialized = false;
        private bool _isJumping = false;
        private bool _hasJumped = false;

        private WaitForSeconds _blinkRate = new WaitForSeconds(.25f);
        private float _blinkInterval = .80f;
        private float _currentBlinkInterval = 0f;
        private bool _blinking = false;
        #endregion

        #region Delegates
        public delegate void PlayerEvent(PlayerController playerController);
        public event PlayerEvent OnPlayerDied;
        public event PlayerEvent OnLevelComplete;
        #endregion

        #region Methods
        #region Unity Hooks
        private void Awake()
        {
            _currentPhase = Phase.White;
            _rb = GetComponent<Rigidbody2D>();
            LookForward();
        }

        private void Update()
        {
            if (!_isInitialized && !_levelStarted)
                return;

            if (_isGrounded && !_followCamera.Initialized){
                _followCamera.ResetCameraPosition();
                _followCamera.Initialized = true;
            }
                
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            HandleFalling();
            
            if (Input.GetKeyUp(KeyCode.K))
            {
                KillPlayer();
            }

            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.LeftShift ) || Input.GetKeyDown(KeyCode.RightShift))
            {
                ChangePhase();
            }

            if (!_isGrounded)
            {
                Vector3 vel = GetComponent<Rigidbody2D>().velocity;
                vel.y -= BONUS_GRAV * Time.deltaTime;
                GetComponent<Rigidbody2D>().velocity = vel;                
            }
            if (_isGrounded && _isJumping)
            {
                _isJumping = false;
                LookForward();
                if (GameManager.PlayAudio)
                    _landSound.Play();
            }            
        }

        private void FixedUpdate()
        {
            if (_levelStarted)
            {
                //_moveInput = Input.GetAxis("Horizontal");
                if (_facingRight == false && _moveInput > 0)
                {
                    Flip();
                }
                else if (_facingRight == true && _moveInput < 0)
                {
                    Flip();
                }

                _rb.velocity = new Vector2(1 * Speed * GetBoostSpeed() * GameManager.Difficulty.ToDifficultySpeedMultiplier(), _rb.velocity.y);
                _isGrounded = Physics2D.OverlapCircle(groundCheck.position, CheckRadius, WhatIsGround);
                if (!_isGrounded && !_isJumping)
                {
                    _isJumping = true;
                }
            }
            else
            {
                _levelStarted = Physics2D.OverlapCircle(groundCheck.position, CheckRadius, WhatIsGround);
            }

            if (!_blinking)
            {
                _currentBlinkInterval += Time.fixedDeltaTime;
                if (_currentBlinkInterval > _blinkInterval)
                {
                    if (Random.Range(0, 10) > 5)
                        StartCoroutine(Blink());
                    _currentBlinkInterval = 0f;
                }                
            }

            if (CurrentBoostTime >= 0){
                CurrentBoostTime -= Time.fixedDeltaTime;
            }
            else{
                IsBoosting = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Spike":
                    KillPlayer();
                    break;
                case "Finish":
                    this.gameObject.SetActive(false);
                    OnLevelComplete?.Invoke(this);
                    break;
                case "BoostArrow":
                    BoostPlayer();
                    break;
                case "ForceWhitePhase":
                    ForceWhitePhase();
                    break;
                case "ForceBlackPhase":
                    ForceBlackPhase();
                    break;
            }
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            _isInitialized = false;
            _followCamera.Initialized = false;
        }
        #endregion

        #region Jump Methods
        public void HandleFalling()
        {
            if (_rb.velocity.y < 0)
            {
                _rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;                
            }
            else if (_rb.velocity.y > 0 && !(Input.GetKey(KeyCode.Space) || Input.GetButton("Jump") || _pressing))
            {
                _rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;                
            }
        }

        private void Jump()
        {
            if (!_isGrounded)
                return;
            _isGrounded = false;
            
            _hasJumped = false;
            _rb.velocity += Vector2.up * Jumpforce;
            LookUp();
            if(GameManager.PlayAudio)
                _jumpSound.Play();
        }
        #endregion

        #region Phazing Methods 
        public void ChangePhase()
        {
            switch (_currentPhase)
            {
                case Phase.White:
                    _currentPhase = Phase.Black;
                    _playerSpriteRenderer.sprite = Phased_Black;
                    _eyesSpriteRenderer.sprite = BlackEyes;
                    break;
                case Phase.Black:
                    _currentPhase = Phase.White;
                    _playerSpriteRenderer.sprite = Phased_White;
                    _eyesSpriteRenderer.sprite = WhiteEyes;
                    break;
            }
            GameManager.ToggleSpikes(_currentPhase);
            GameManager.TogglePlatforms(_currentPhase);
        }
        #endregion Phazing Methods (end)

        public void InitializePlayer(Vector2 startingPosition)
        {
            _isInitialized = true;
            gameObject.SetActive(true);
            transform.position = startingPosition;
            //_followCamera.Initialized = true;
            LookForward();
        }

        #region Pointer Events
        public void OnPointerDown(BaseEventData eventData)
        {
            Jump();
            _pressing = true;
        }

        public void OnPointerUp(BaseEventData eventdata)
        {
            _pressing = false;
        }
        #endregion Pointer Events (end)

        #region Visuals

        private void LookUp()
        {
            _eyesSpriteRenderer.transform.localPosition = EyeLookingUpLocation;
        }

        private void LookForward()
        {
            _eyesSpriteRenderer.transform.localPosition = Vector3.zero;
        }

        private IEnumerator Blink()
        {
            _eyesSpriteRenderer.sprite = _currentPhase == Phase.White ? WhiteEyesBlink : BlackEyesBlink;
            yield return _blinkRate;
            _eyesSpriteRenderer.sprite = _currentPhase == Phase.White ? WhiteEyes : BlackEyes;
        }
        #endregion Visuals (end)

        #region Private Methods
        private void Flip()
        {
            _facingRight = !_facingRight;
            var scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
       
        private void KillPlayer()
        {
            IsBoosting = false;
            gameObject.SetActive(false);            
            _levelStarted = false;
            switch (_currentPhase)
            {
                case Phase.White:
                    Death_Effect_White.transform.position = transform.position;
                    Death_Effect_White.Play();
                    break;
                case Phase.Black:
                    Death_Effect_Black.transform.position = transform.position;
                    Death_Effect_Black.Play();
                    break;
            }            
            OnPlayerDied?.Invoke(this);
        }

        private float GetBoostSpeed()
        {
            return IsBoosting ? GameManager.GetBoostSpeed() : 1f;
        }

        private void BoostPlayer()
        {
            IsBoosting = true;
            CurrentBoostTime = GameManager.GetBoostSpeed();
        }

        private void ForceBlackPhase()
        {
            _currentPhase = Phase.Black;
            _playerSpriteRenderer.sprite = Phased_Black;
            _eyesSpriteRenderer.sprite = BlackEyes;
            GameManager.DisablePhaseButton();
            GameManager.TogglePlatforms(_currentPhase);
            GameManager.ToggleSpikes(_currentPhase);
        }

        private void ForceWhitePhase()
        {
            _currentPhase = Phase.White;
            _playerSpriteRenderer.sprite = Phased_White;
            _eyesSpriteRenderer.sprite = WhiteEyes;
            GameManager.DisablePhaseButton();
            GameManager.TogglePlatforms(_currentPhase);
            GameManager.ToggleSpikes(_currentPhase);
        }
        #endregion
        #endregion       
    }
}