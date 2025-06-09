using System;
using System.Collections.Generic;
using Interactables;
using Interfaces;
using PrimeTween;
using Sirenix.OdinInspector;
using Sounds;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

namespace PlayerLogic
{
    public class Player : MonoBehaviour, IDamageable
    {
        public static Player Instance;
        
        private PlayerVFX _playerVFX;
        private Animator _playerAnimator;
        private Rigidbody _playerRigidbody;
        private SeedDataSO _currentSeedData;
        private PlayerInput _playerInput;
        private PlayerMovement _playerMovement;
        
        [SerializeField]
        private bool _hasObtainDash;
        [SerializeField]
        private bool _hasObtainDoubleJump;
        [SerializeField]
        private bool _hasObtainPush;
        [SerializeField] 
        private float _respawnDelay = 3f;
        [SerializeField] 
        private float _invulnerableDuration = 1f;
        
        [SerializeField] 
        public bool _skipIntro;
        
        [ShowInInspector,ReadOnly]
        public List<PlayerInteractable> Interactables { get; set; } = new();
        
        [ShowInInspector, ReadOnly] 
        private PlayerInteractable _playerInteractable;
        
        public PlayerMovement PlayerMovement { get => _playerMovement; private set=>_playerMovement = value; }
        public bool HasObtainDash { get => _hasObtainDash;  set => _hasObtainDash = value; }
        public bool HasObtainDoubleJump { get => _hasObtainDoubleJump;   set => _hasObtainDoubleJump = value; }
        public bool HasObtainPush { get => _hasObtainPush;   set => _hasObtainPush = value; }
        public Vector3 LastCheckPoint { get; set; }
        public PlayerInput PlayerInput { get=>_playerInput; private set =>_playerInput = value; }

        public static Action OnObtainPush;
        
        public SeedDataSO CurrentSeedData
        {
            get => _currentSeedData;
            set
            {
                _currentSeedData = value;
                _playerVFX.UpdateSeedData(_currentSeedData);
            }
        }
        
        public PlayerVFX PlayerVFX
        {
            get => _playerVFX;
            set => _playerVFX = value;
        }
        
        public PlayerInteractable CurrentInteractable
        {
            get => _playerInteractable;
            set => _playerInteractable = value;
        }

        public Action OnDead;
        
        private static readonly int IsDead = Animator.StringToHash("isDead");
        private bool _isInvul;

        private void Awake()
        {
            Time.timeScale = 1;
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _playerMovement = GetComponent<PlayerMovement>();
            _playerInput = GetComponent<PlayerInput>();
            _playerVFX = GetComponent<PlayerVFX>();
            _playerAnimator = GetComponent<Animator>();
            _playerRigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            OnDead += Respawn;
            GlobalEvents.Instance.OnGameEnd += () => InputManager.Instance.DisableInputAction("Movement");
            ImageCutscene.OnCutsceneFinish += PlayerSetup;
            ImageCutscene.OnCutsceneStart += ShowPlayer;
        }

        private void OnDisable()
        {
            OnDead -= Respawn;
            GlobalEvents.Instance.OnGameEnd -= () => InputManager.Instance.DisableInputAction("Movement");
            ImageCutscene.OnCutsceneFinish -= PlayerSetup;
            ImageCutscene.OnCutsceneStart -= ShowPlayer;
        }

        private void Start()
        {
            LastCheckPoint = transform.position;
            if (HasObtainDoubleJump)
            {
                ObtainDoubleJump();
            }

            if (_skipIntro)
            {
                PlayerSetup();
            }
        }

        public void ObtainDoubleJump()
        {
            PlayerMovement.SetJumpAmount(2);
            HasObtainDoubleJump = true;
        }

        private void Respawn()
        {
            _isInvul = true;
            PlayerMovement.HorizontalInput = 0;
            SoundManager.Instance.PlaySfx("Die", transform.position);
            _playerRigidbody.linearVelocity = Vector3.zero;
            _playerAnimator.SetBool(IsDead, true);
            _playerInput.DeactivateInput();
            Tween.Delay(_respawnDelay, () =>
            {
                _playerAnimator.SetBool(IsDead, false);
                TeleportToCheckPoint();
                SoundManager.Instance.PlaySfx("Respawn", transform.position);
                VfxManager.Instance.SpawnFromPool("Respawn", transform.position);
                _playerAnimator.Play("Idle");
                _playerInput.ActivateInput();
                Tween.Delay(_invulnerableDuration, () =>
                {
                    _isInvul = false;
                });
            });
        }

        public void TeleportToCheckPoint()
        {
            transform.position = LastCheckPoint;
        }

        public void TakeDamage()
        {
            if(_isInvul) return;
            Respawn();
        }

        private void ShowPlayer()
        {
            GetComponent<SpriteRenderer>().enabled = true;
            _playerAnimator.SetLayerWeight(1,0);
        }

        public void PlayerSetup()
        {
            PlayerInput.actions.FindActionMap("Movement").Enable();
            PlayerMovement.enabled=true;
            PlayerInput.actions.FindActionMap("Pause").Enable();
        }

        public void AddInteractables(PlayerInteractable interactable)
        {
            Interactables.Add(interactable);
            UpdateInteractables();
        }

        public void RemoveInteractables(PlayerInteractable interactable)
        {
            if (!Interactables.Contains(interactable)) return;

            Interactables.Remove(interactable);

            if (interactable == CurrentInteractable)
            {
                CurrentInteractable.UnsubscribeInteraction();
                CurrentInteractable = null;

                if (Interactables.Count > 0)
                {
                    UpdateInteractables();
                }
            }
    
            if (CurrentInteractable == null && Interactables.Count > 0)
            {
                CurrentInteractable = Interactables[^1];
                CurrentInteractable.SubscribeInteraction();
            }
        }

        private void UpdateInteractables()
        {
            PlayerInteractable lastInteractable = Interactables[^1];

            if (CurrentInteractable != lastInteractable)
            {
                CurrentInteractable?.UnsubscribeInteraction();
                CurrentInteractable = lastInteractable;
                CurrentInteractable.SubscribeInteraction();
            }
        }
    }
}