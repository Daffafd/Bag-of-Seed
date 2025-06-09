using System;
using System.Collections;
using System.Collections.Generic;
using Interactables;
using UnityEngine;

public class ChiliSeedSpawner : MonoBehaviour
{
    [SerializeField] private Seed _seed;
    private void OnEnable()
    {
        GlobalEvents.Instance.OnChiliExplode += OnExplode;
        BossPhaseManager.Instance.OnEnterChiliStage += OnChangeState;
    }

    private void OnDisable()
    {
        GlobalEvents.Instance.OnChiliExplode -= OnExplode;
        BossPhaseManager.Instance.OnEnterChiliStage -= OnChangeState;
    }

    private void OnExplode()
    {
        SpawnSeed();
    }

    private void OnChangeState(Phase phase)
    {
        SpawnSeed();
    }

    private void SpawnSeed()
    {
        
        if (BossPhaseManager.CurrentPhase == Phase.Chili)
        {
            Instantiate(_seed,transform.position, Quaternion.identity);
        }
    }
}