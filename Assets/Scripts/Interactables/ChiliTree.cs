using Interactables;
using Interfaces;
using PrimeTween;
using Sirenix.OdinInspector;
using Sounds;
using UnityEngine;
using UnityEngine.Events;

public class ChiliTree : MonoBehaviour
{
    [Header("VFX Explosion")]
    [SerializeField] private GameObject vfxExplosion;

    [SerializeField] private Transform _explosionCenter;

    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explodeCountDown;
    [SerializeField] private LayerMask _layerMask;
    private Collider[] _colliders = new Collider[20];

    public UnityEvent OnExplode;

    private bool _hasBoss;

    [ShowInInspector,ReadOnly]
    private Wither _wither;

    private PlantArea _plantArea;
    // Start is called before the first frame update
    void Start()
    {
        var colliders = Physics.OverlapSphere(_explosionCenter.position, 2f);
        foreach (var c in colliders)
        {
            if (c.TryGetComponent(out PlantArea plantArea) && _plantArea==null)
            {
                _plantArea = plantArea;
            }
            if (c.TryGetComponent(out Wither wither))
            {
                _wither = wither;
            }
        }
        
        Tween.Delay(_explodeCountDown, Explode);
    }
    private void Explode()
    { 
        InputManager.Instance?.Rumble();
        Instantiate(vfxExplosion, transform.position, transform.rotation);
        SoundManager.Instance.PlaySfx("Explode", transform.position);
        Physics.OverlapSphereNonAlloc(_explosionCenter.position, _explosionRadius,_colliders,_layerMask);
        if (_colliders.Length > 0)
        {
            foreach (var col in _colliders)
            {
                if (!col) continue;
                if (col.TryGetComponent(out WormBoss boss))
                {
                    _hasBoss = true;
                    if (_wither)
                    {
                        _wither.ChangeMaterial();
                    }
                    Destroy(_plantArea.transform.parent.gameObject);
                }
                if (col.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage();
                }
            }
        }
        if(!_hasBoss)
            GlobalEvents.Instance.OnChiliExplode?.Invoke();
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_explosionCenter.position,_explosionRadius);
    }
}
