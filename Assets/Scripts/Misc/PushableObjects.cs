using System;
using PlayerLogic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class PushableObjects : MonoBehaviour
{
    private Rigidbody _rb;
    private AudioSource _audioSource;

    [ShowInInspector,ReadOnly]
    private bool _isHitLimiter;

    [SerializeField] private bool _useCustomGravity;

    [SerializeField, ShowIf("_useCustomGravity")]
    private float _gravityForce = 300f;

    [SerializeField] private bool _shouldLockY;

    [SerializeField] private MeshRenderer _objectMeshRenderer;
    [SerializeField] private GameObject _spotlightParent;

    private float _initialIntensity;
    private static readonly int EmmisiveIntensity = Shader.PropertyToID("_EmissiveIntensity");

    public bool ShouldLockY
    {
        get => _shouldLockY;
        set => _shouldLockY = value;
    }

    private void OnEnable()
    {
        Player.OnObtainPush += ActivateVisualCue;
    }

    private void OnDisable()
    {
        Player.OnObtainPush -= ActivateVisualCue;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _initialIntensity = _objectMeshRenderer.material.GetFloat(EmmisiveIntensity);
        DeactivateVisualCue();
    }

    private void Update()
    {
        if ( Mathf.Abs(_rb.linearVelocity.x) >= 1f && !_audioSource.isPlaying)
        {
            
            _audioSource.Play();
        }
        
        if (Mathf.RoundToInt(_rb.linearVelocity.x) == 0 || _isHitLimiter)
        {
            _audioSource.Stop();
        }
    }

    private void FixedUpdate()
    {
        if (_useCustomGravity&&_rb.linearVelocity.y < 0)
        {
            _rb.AddForce(Vector3.down *_gravityForce);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Limiter"))
        {
            _rb.linearVelocity = Vector3.zero;
            _isHitLimiter = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Limiter"))
        {
            _isHitLimiter = false;
        }
    }

    private void ActivateVisualCue()
    {
        if (!_objectMeshRenderer) return;
        Material _material = _objectMeshRenderer.material;
        if(!_material) return;
        if(!_spotlightParent) return;
        
        _material.SetFloat(EmmisiveIntensity,_initialIntensity);
        var lights = _spotlightParent.GetComponentsInChildren<Light>();
        foreach (var light in lights)
        {
            light.enabled = true;
        }
        
        HDMaterial.ValidateMaterial(_material);
    }

    private void DeactivateVisualCue()
    {
        Material _material = _objectMeshRenderer.material;
        if(!_material) return;
        if(!_spotlightParent) return;
    
        _material.SetFloat(EmmisiveIntensity,0);
        
        var lights = _spotlightParent.GetComponentsInChildren<Light>();
        foreach (var light in lights)
        {
            light.enabled = false;
        }
        
        HDMaterial.ValidateMaterial(_material);
    }
}
