using System;
using PlayerLogic;
using Sounds;
using UnityEngine;
using Utility;

namespace Interactables
{
    public class PlantArea : PlayerInteractable
    {
        [SerializeField] private SeedDataSO _seedData;
    
        [SerializeField] private bool _isPlanted;

        private Animator _animator;

        public bool IsPlanted => _isPlanted;

        private int _speed;

        [SerializeField] private Transform _animationTransform;

        [SerializeField] private SeedDataSO _requiredSeed;

        [SerializeField] private bool _isOneTimeUse;

        [SerializeField] private float _returnToSeedDelay;

        [SerializeField] private Transform _vfxSpawnPoint;


        private float _timer;
        private GameObject _currPlant;

        public Action OnFinishGrow;

        private bool _hasPlayGrowSfx;

        // Start is called before the first frame update
        public override void  Start()
        {
            base.Start();
            _animator = _animationTransform.GetComponent<Animator>();
            _speed = Animator.StringToHash("Speed");
        }
    
        public override void Interact()
        {
        
            if (_currPlant != null || _isPlanted) return;

            var currentSeedData = Player.Instance.CurrentSeedData;
            var vfxType = currentSeedData == null ? "EmptySeed" : "WrongSeed";
            if (currentSeedData == null || !Equals(currentSeedData.name, _requiredSeed.name))
            {
                SoundManager.Instance.PlaySfx("FailPlant", transform.position);

                VfxManager.Instance.SpawnFromPool(vfxType, _vfxSpawnPoint.position, Quaternion.identity,2f);

                return;
            }
        
            SoundManager.Instance.PlaySfx("Plant", transform.position);
            _interactUI.SetActive(false);
            _seedData = Player.Instance.CurrentSeedData;
            Player.Instance.CurrentSeedData = null;
            _animator.runtimeAnimatorController = _seedData.AnimatorController;
        }

        private void Update()
        {
            _isPlanted = _currPlant != null || _seedData;
        
            if(!_animator.runtimeAnimatorController) return;
            if ( _animator.GetCurrentAnimatorStateInfo(0).normalizedTime<=0)
            {
                _timer += Time.deltaTime;
                if (_timer >= _returnToSeedDelay)
                {
                    SoundManager.Instance.PlaySfx("PopSeed", transform.position);
                
                    Instantiate(_seedData.SeedPrefab,
                        new Vector3(transform.position.x, transform.position.y, _seedData.SeedPrefab.transform.position.z),
                        Quaternion.identity);
                
                    _hasPlayGrowSfx = false;
                    _seedData = null;
                    _animator.runtimeAnimatorController = null;
                    _isPlanted = false;
                    _timer = 0;
                }
            }

            if (!(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)) return;
            OnFinishGrow?.Invoke();
            if(_seedData != null && _seedData.ObjectToInstantiate)
                _currPlant = Instantiate(_seedData.ObjectToInstantiate, _animationTransform.position, Quaternion.identity);
        
            if (_isOneTimeUse)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                _animator.runtimeAnimatorController = null;
                _seedData = null;
            }
        }

        public override void OnTriggerStay(Collider other)
        {
            if (Player.Instance.CurrentInteractable == this)
            {
                if (!other.TryGetComponent(out Player player) || _isPlanted)
                {
                    UnsubscribeInteraction();
                }
                else if(player && !_isPlanted)
                {
                    SubscribeInteraction();
                }
            }
        }

        public void PlayAnim()
        {
            if (!_hasPlayGrowSfx)
            {
                SoundManager.Instance.PlaySfx(_seedData.GrowSfxId, transform.position);
                _hasPlayGrowSfx = true;
            }
            _animator.SetFloat(_speed, 1);
        }

        public void PauseAnim()
        {
            _animator.SetFloat(_speed, 0);
        }
    }
}
