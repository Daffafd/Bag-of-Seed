using System;
using PrimeTween;
using Sounds;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpecialPlatform :MonoBehaviour
{
    [SerializeField] private Transform _stopPoint1;
    [SerializeField] private Transform _stopPoint2;

    [SerializeField] private float _stayDuration = 5f;

    private bool _canInteract;

    [FormerlySerializedAs("_enter")] [FormerlySerializedAs("_masuk")] 
    [SerializeField] private AudioSource _enterAudioSource;
    
    [FormerlySerializedAs("_keluar")] 
    [SerializeField] private AudioSource _exitAudioSource;

    [SerializeField] private Material _material;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissiveColor");

    private float _currentTimer;
    private bool _isActive;
    
    [SerializeField]
    private AudioSource _audioSource;

    private static bool _sfxPlayed;
    private void OnEnable()
    {
        BossPhaseManager.Instance.OnEnterChiliStage += ChangeUI;
        BossPhaseManager.Instance.OnEnterWillowStage += ChangeUI;
    }

    private void OnDisable()
    {
        BossPhaseManager.Instance.OnEnterChiliStage -= ChangeUI;
        BossPhaseManager.Instance.OnEnterWillowStage -= ChangeUI;
    }

    public void ChangeUI(Phase phase)
    {
        _material.EnableKeyword("_Emission");
        _material.SetColor(EmissionColor, phase == Phase.Willow ? Color.green * 2 : Color.red* 2);
        _canInteract = phase == Phase.Willow;
    }

    private void Update()
    {
        if (_isActive)
        {
            _currentTimer += Time.deltaTime;
            
            if (_currentTimer > _stayDuration-4 && _audioSource && !_audioSource.isPlaying)
            {
                if (_audioSource)
                {
                    _audioSource.Play();
                }
                    
            }

            if (_currentTimer > _stayDuration)
            {
                if (_enterAudioSource && !_sfxPlayed)
                {
                    _sfxPlayed = true;
                    _enterAudioSource.Play();
                }

                Tween.Position(transform, _stopPoint1.position, 1f)
                    .OnComplete(() =>
                    {
                        _canInteract = BossPhaseManager.CurrentPhase == Phase.Willow;
                        _sfxPlayed = false;
                    });
                _isActive = false;
            }
        }
    }

    public void Activate()
    {
        if(_audioSource && _audioSource.isPlaying)
            _audioSource.Stop();
        _currentTimer = 0;
        _isActive = true;
        if (Vector3.Distance(transform.position, _stopPoint2.position) > 1)
        {
            
            if (_exitAudioSource && !_sfxPlayed)
            {
                _exitAudioSource.Play();
                _sfxPlayed = true;
            }

            Tween.Position(transform, _stopPoint2.position, 1f).OnComplete(() => _sfxPlayed = false);
        }
    }
}