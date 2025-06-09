using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using PrimeTween;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerLogic
{
    [System.Serializable]
    public struct AttackData
    {
        public float Duration;
        public float InputWindow;
    }
    
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _playerControl;
        private Animator _animator;

        [SerializeField] private Transform _playerAttackPoint;
        [SerializeField] private float _playerAttackRadius;

        [ShowInInspector]
        private int _comboIndex;

        private Tween _attackTween;
        private bool _isAttacking;
        private bool _inputWindow;

        private List<int> _inputList = new List<int>();

        [SerializeField] private List<AttackData> _comboAttackData = new List<AttackData>();
        private void OnEnable()
        {
            _playerControl.FindActionMap("Combat").Enable();
        }

        private void OnDisable()
        {
            _playerControl.FindActionMap("Combat").Disable();
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        private void Update()
        {
            if (_inputList.Count > 0)
            {
                if (!_isAttacking)
                {
                    var input = _inputList[0];
                    if (input == 0)
                    {
                        ExecuteAttack();
                    }

                    if (_inputList.Count > 0)
                    {
                        _inputList.Remove(0);
                    }
                }
            
            }
        }
        
        private void OnAttack()
        {
            TryAttack();
        }

        private void ExecuteAttack()
        {
            if(_attackTween.isAlive) _attackTween.Stop();
            if (_comboIndex ==_comboAttackData.Count) _comboIndex = 0;
            _inputWindow = false;
            Tween.Delay(_comboAttackData[_comboIndex].Duration/1.1f, () =>
            {
                _isAttacking = false;
            });
            _attackTween = Tween.Delay(_comboAttackData[_comboIndex].Duration, () =>
            {
                _isAttacking = false;
                _comboIndex = 0;
                _animator.SetInteger("comboIndex",_comboIndex);
            });
            Tween.Delay(_comboAttackData[_comboIndex].InputWindow , () => _inputWindow = true);
            _isAttacking = true;
            
            _animator.SetInteger("comboIndex", _comboIndex);
            _comboIndex++;
            
            _animator.SetTrigger("Attack");
            _animator.SetBool("isAttacking",true);
        }
        
        private void TryAttack()
        {
            if (!_isAttacking)
            {
                ExecuteAttack();
            }
            else if(_inputList.Count < 1 && _inputWindow)
            {
                _inputList.Add(0);
            }
        }
        
    }
}
