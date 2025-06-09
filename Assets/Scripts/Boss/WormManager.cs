using System.Collections.Generic;
using Cinemachine;
using PrimeTween;
using Sirenix.OdinInspector;
using Sounds;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WormManager : MonoBehaviour
{
    [SerializeField] private Transform _groundShake;
    [SerializeField] private float _interval = 3f;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    public float LastTime { get; set; }
    private bool _isComplete = true;
    [SerializeField] private float _cameraShakeDuration;

    [ShowInInspector,ReadOnly]
    private bool _isInitial =true;
    
    [ShowInInspector,ReadOnly]
    public Vector3 SpawnBossPos { get; set; }

    public bool IsWormDead { get; set; }
    public bool IsComplete
    {
        get => _isComplete;
        set => _isComplete = value;
    }
    
    [ShowInInspector,ReadOnly]
    public float SpawnLastLogYPos { get; set; }

    [SerializeField] private List<GameObject> _gameObjects;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GlobalEvents.Instance.OnBossDefeated += BossDefeated;
    }

    private void OnDisable()
    {
        GlobalEvents.Instance.OnBossDefeated -= BossDefeated;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isInitial && Time.time >= LastTime +_interval && IsComplete && !IsWormDead)
        {
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        SoundManager.Instance.PlaySfx("Groundshake", transform.position);
        _groundShake.position = SpawnBossPos;
        _groundShake.gameObject.SetActive(true);
        _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
        Tween.Custom(1, 0,_cameraShakeDuration,
            (value) =>
                _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain =
                    value);
        IsComplete = false;
    }

    private void BossDefeated()
    {
        foreach (var o in _gameObjects)
        {
            Destroy(o);
        }
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_isInitial)
        {
            Tween.Delay(5f, () =>
            {
                SpawnBoss();
                _isInitial = false;
            });
        }
    }
}
