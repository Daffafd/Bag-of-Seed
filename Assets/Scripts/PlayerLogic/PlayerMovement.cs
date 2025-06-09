using System;
using PrimeTween;
using Sirenix.OdinInspector;
using Sounds;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;
using VFX;

namespace PlayerLogic
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody _rb;
        public TrailVFX trailVFXScript;

        [SerializeField] private GameObject _walkTrail;

        [Header("Horizontal Movement")]
        [SerializeField] private float _moveSpeed;

        [SerializeField] private float _pushForce;
        
        [Header("Vertical Movement"), Tooltip("Additional gravity to control fall speed")]
        [SerializeField] private float _additionalGravity;
        [SerializeField] private bool _clampFallSpeed;
        [SerializeField, ShowIf("_clampFallSpeed")] private float _maxFallSpeed;
        
        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundcheckRadius;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _coyoteTime = 1f;
        [SerializeField] private int _jumpCount = 2;
        
        [Header("Dash")]
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _dashCooldown;
        [SerializeField] private float _dashDuration;
        private int _dashCount = 1;
        
        [Space(10)]
        [SerializeField] private InputActionAsset _playerControl;
        
        private SpriteRenderer _sr;
        private float _lastDashTime;
        private Animator _animator;
        private bool _isDashing;
        private float _horizontalInput;
        private bool _allowMove;
        private bool _allowRotation;
        
        [ShowInInspector, ReadOnly]
        private float _coyoteTimer;
        
        [ShowInInspector, ReadOnly]
        private bool _isGrounded;

        public bool IsGrounded => _isGrounded;

        public float HorizontalInput
        {
            get => _horizontalInput;
            set => _horizontalInput = value;
        }

        [ShowInInspector, ReadOnly] private int _currJumpCount;

        private readonly int isWalking = Animator.StringToHash("isWalking");
        private readonly int isDashing = Animator.StringToHash("isDashing");
        private readonly int yVelocity = Animator.StringToHash("yVelocity");
        private readonly int isGrounded = Animator.StringToHash("isGrounded");
        private readonly int isPushing = Animator.StringToHash("isPushing");

        [SerializeField]
        private AudioSource _audioSource;

        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _sr = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _lastDashTime -= _dashCooldown;
            _currJumpCount = _jumpCount;

        }

        private void OnEnable()
        {
            _playerControl.FindActionMap("Movement").Enable();
        }

        private void OnDisable()
        {
            _playerControl.FindActionMap("Movement").Disable();
        }

        // Update is called once per frame
        void Update()
        {
            trailVFXScript.makeTrail = _animator.GetBool(isDashing);

            HandleMovementAnimation();
            HandleGroundCheck();
            if (_rb.linearVelocity.y < 0)
            {
                _coyoteTimer += Time.deltaTime;
            }

            if (_rb.linearVelocity.y > 0)
            {
                _coyoteTimer = _coyoteTime;
            }

            if (_isGrounded)
            {
                _coyoteTimer = 0;
            }
            
            if(_isDashing) return;
            HandleFlip();
            _rb.linearVelocity = new Vector2(HorizontalInput * _moveSpeed , _rb.linearVelocity.y);

            if (_animator.GetBool(isWalking)&&!_audioSource.isPlaying && IsGrounded)
            {
                _audioSource.Play();
               
            }

            if (_animator.GetBool(isWalking)&&IsGrounded)
            {
                VfxManager.Instance.SpawnFromPool("Walk", _groundCheck.position, Quaternion.identity,.5f);
            }
        }

        private void FixedUpdate()
        {
            if (_rb.linearVelocity.y < 0)
            {
                _rb.AddForce(Vector3.down*_additionalGravity, ForceMode.Force);
                if (!_clampFallSpeed) return;
                var vector3 = _rb.linearVelocity;
                vector3.y = Mathf.Clamp(_rb.linearVelocity.y, 0, _maxFallSpeed);
                _rb.linearVelocity = vector3;
            }
        }

        private void OnJump()
        {
            if ((IsGrounded||_coyoteTimer < _coyoteTime ||Player.Instance.HasObtainDoubleJump) && _currJumpCount > 0)
            {
                SoundManager.Instance.PlaySfx("Jump",transform.position);
                _currJumpCount--;
                _coyoteTimer = _coyoteTime;
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
                VfxManager.Instance.SpawnFromPool("Jump", _groundCheck.position, Quaternion.identity, .5f);
            }
        }

        private void OnJumpRelease()
        {
            if(_rb.linearVelocity.y>0)
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y * .5f);
        }

        private void OnMove()
        {
            HorizontalInput = _playerControl.FindAction("Move").ReadValue<float>();
        }

        private void HandleFlip()
        {
            transform.localEulerAngles = HorizontalInput switch
            {
                > 0 => new Vector3(0, 0, 0),
                < 0 => new Vector3(0, 180, 0),
                _ => transform.localEulerAngles
            };
        }

        private void HandleMovementAnimation()
        {
            _animator.SetBool(isWalking, HorizontalInput != 0);
            _animator.SetBool(isGrounded, IsGrounded);
            _animator.SetFloat(yVelocity, _rb.linearVelocity.y);
        }

        private void HandleGroundCheck()
        {
            
            var groundColliders = Physics.OverlapSphere(_groundCheck.position, _groundcheckRadius,_groundLayer);
            if(_isGrounded == groundColliders.Length > 0) return;
            _isGrounded = groundColliders.Length > 0 || _rb.linearVelocity.y==0 ;
            if (_isGrounded)
            {
                _currJumpCount = _jumpCount;
                _dashCount = 1;
            }
        }

        private void OnDash()
        {
            if (Time.time > _lastDashTime + _dashCooldown && Player.Instance.HasObtainDash && _dashCount>0) 
            {
                SoundManager.Instance.PlaySfx("Dash",transform.position);
                Dash(new Vector3(HorizontalInput, 0,0));
            }
        }
        
        private void Dash(Vector3 direction)
        {
            if(Mathf.Abs(_rb.linearVelocity.y)>0.1)
                _dashCount--;
            
            _rb.linearVelocity = Vector3.zero;

            _rb.useGravity = false;

            _lastDashTime = Time.time;

            _isDashing = true;
            _animator.SetBool(isDashing,_isDashing);
            
            if (direction.x == 0)
            {
                
                direction =transform.localEulerAngles.y==0 ? new Vector3(1,0,0) : new Vector3(-1,0,0);
            }
            
            _rb.linearVelocity = direction * _dashSpeed + new Vector3(0, _rb.linearVelocity.y > 0 ? _rb.linearVelocity.y : 0,0);
            _sr.color = new Color(1, 1, 1, 0.75f);

            Tween.Delay(_dashDuration, () =>
            {
                _rb.useGravity = true;
                _sr.color = new Color(1, 1, 1, 1f);
                _isDashing = false;
                _animator.SetBool(isDashing,_isDashing);
            });
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position,_groundcheckRadius);
        }

        public void SetJumpAmount(int amount)
        {
            _jumpCount = amount;
            if(_rb.linearVelocity.y==0)
                _currJumpCount = _jumpCount;
            else
            {
                _currJumpCount++;
            }
        }

        public void SetIsPushing(bool push)
        {
            _animator.SetBool(isPushing, push);
        }

        private void OnCollisionStay(Collision other)
        {
            if (!other.collider.TryGetComponent(out PushableObjects obj)) return;
            
            bool canPush = Mathf.Abs(other.GetContact(0).normal.y) < 0.1f && Player.Instance.HasObtainPush && IsGrounded && !_isDashing;

            if (canPush)
            {
                SetIsPushing(true);
                SetRigidbodyConstraints(other.collider.attachedRigidbody, obj.ShouldLockY);

                other.collider.attachedRigidbody.linearVelocity = new Vector3(-other.GetContact(0).normal.x * _pushForce, 0, 0);
            }
            else if (!IsGrounded || _isDashing || _horizontalInput == 0)
            {
                if (!_animator) return;

                SetIsPushing(false);
                ResetRigidbodyConstraints(other.collider.attachedRigidbody, obj.ShouldLockY);

                var otherRigidbody = other.collider.attachedRigidbody;
                otherRigidbody.linearVelocity = new Vector3(0, otherRigidbody.linearVelocity.y);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (!other.collider.TryGetComponent(out PushableObjects obj)) return;

            SetIsPushing(false);
            ResetRigidbodyConstraints(other.collider.attachedRigidbody, obj.ShouldLockY);

            other.collider.attachedRigidbody.linearVelocity = Vector3.zero;
        }

        private void SetRigidbodyConstraints(Rigidbody rb, bool shouldLockY)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            if (shouldLockY)
            {
                rb.constraints |= RigidbodyConstraints.FreezePositionY;
            }
        }

        private void ResetRigidbodyConstraints(Rigidbody rb, bool shouldLockY)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            if (shouldLockY)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
            }
            else
            {
                rb.constraints |= RigidbodyConstraints.FreezePositionX;
            }
        }

    }
}
