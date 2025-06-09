using Cinemachine;
using PrimeTween;
using UnityEngine;

public class BossHead : MonoBehaviour
{
    private Tween _tween;
    [SerializeField] private CinemachineVirtualCamera _targetCamera;
    [SerializeField] private float _shakeAmplitude = 1 ;
    [SerializeField] private float _shakeFrequency = 1;

    private CinemachineBasicMultiChannelPerlin _perlin;

    private void Start()
    {
        _perlin = _targetCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
}
