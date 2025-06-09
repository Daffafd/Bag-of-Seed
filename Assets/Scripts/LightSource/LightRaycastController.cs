using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class LightRaycastController : MonoBehaviour
{
    private readonly List<LightRaycast> _lightRaycasts = new();

    public Action<bool> OnHitObject;

    private int _hitCount;
    
    [ShowInInspector,ReadOnly]
    public int HitCount
    {
        get => _hitCount;
        set => _hitCount = value;
    }

    private void OnEnable()
    {
        OnHitObject += UpdateHitCount;
    }

    private void OnDisable()
    {
        OnHitObject -= UpdateHitCount;
    }

    private void Start()
    {
        foreach (var l in transform.GetComponentsInChildren<LightRaycast>())
        {
            _lightRaycasts.Add(l);
        }
    }

    private void UpdateHitCount(bool hitPlantArea)
    {
        HitCount = hitPlantArea
            ? Mathf.Clamp(HitCount + 1, 0, _lightRaycasts.Count)
            : Mathf.Clamp(HitCount - 1, 0, _lightRaycasts.Count);
    }
}