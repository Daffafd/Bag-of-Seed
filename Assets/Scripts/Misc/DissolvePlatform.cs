using Interactables;
using PrimeTween;
using Sirenix.OdinInspector;
using Sounds;
using UnityEngine;
using UnityEngine.Events;

public class DissolvePlatform : MonoBehaviour
{
    [SerializeField] private PlantArea _willowPlantArea;
    
    [ShowInInspector,ReadOnly]
    private Collider _collider;
    [SerializeField] private float _colliderDissapearTime = 1f; 

    public UnityEvent OnFinishDissolve;

    private static readonly int DissolveAmount = Shader.PropertyToID("_Dissolve_Amount");
    // Start is called before the first frame update

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    void OnEnable()
    {
        _willowPlantArea.OnFinishGrow += Dissolve;
    }

    private void OnDisable()
    {
        _willowPlantArea.OnFinishGrow -= Dissolve;
    }

    private void Dissolve()
    {
        SoundManager.Instance.PlaySfx("Dissolve", transform.position);
        var meshRenderer = GetComponent<MeshRenderer>();
        Tween.Delay(_colliderDissapearTime, () => _collider.isTrigger = true);
        Tween.Custom(0, .9f, 2f, (value) => meshRenderer.material.SetFloat(DissolveAmount, value))
            .OnComplete(() =>
            {
                OnFinishDissolve?.Invoke();
                Destroy(gameObject);
            });
    }
}
