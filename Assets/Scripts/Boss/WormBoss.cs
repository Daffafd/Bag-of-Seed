using Interfaces;
using Sirenix.OdinInspector;
using Sounds;
using UnityEngine;
using UnityEngine.Playables;
using Utility;

public class WormBoss : MonoBehaviour, IDamageable 
{
    [SerializeField] private int _health;
    [SerializeField] private Transform _vfxSpawnTransform;
    private Animator _animator;
    [ShowInInspector,ReadOnly]
    private int _currHealth;
    private WormManager _manager;
    [SerializeField] private GameObject _lastLog;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _manager = FindFirstObjectByType<WormManager>();
        _currHealth = _health;
    }

    private void OnEnable()
    {
        _animator.SetInteger("Lives", _currHealth);
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.K))
        // {
        //     TakeDamage();
        // }
    }

    public void TakeDamage()
    {
        VfxManager.Instance.SpawnFromPool("BossHurt", _vfxSpawnTransform.position, Quaternion.identity);
        _currHealth--;
        _animator.SetInteger("Lives",_currHealth);
        if (_currHealth == 0)
        {
            SoundManager.Instance.PlaySfx("BossDie", transform.position);
            Instantiate(_lastLog, new Vector3(transform.position.x,_manager.SpawnLastLogYPos,_lastLog.transform.position.z) , Quaternion.identity);
            GlobalEvents.Instance.OnBossDefeated?.Invoke();
            _manager.IsWormDead = true;
        }
        BossPhaseManager.Instance.EnterWillowStage();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable) &&other.CompareTag("Player"))
        {
            damageable.TakeDamage();
        }
    }

    public void FinishAnimation()
    {
        SoundManager.Instance.PlaySfx("HideRumble", transform.position);
        _manager.IsComplete = true;
        _manager.LastTime = Time.time;
        gameObject.SetActive(false);
    }

    public void PlayShowSfx()
    {
        SoundManager.Instance.PlaySfx("BossSpawn", transform.position);
    }

    public void PlayRoarSfx()
    {
        SoundManager.Instance.PlaySfx("Roar", transform.position, .7f);
    }

    public void PlayHideSfx()
    {
        SoundManager.Instance.PlaySfx("BossHide", transform.position);
    }
}
