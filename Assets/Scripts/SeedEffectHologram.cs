using System;
using System.Collections.Generic;
using Interactables;
using UnityEngine;

public class SeedEffectHologram : MonoBehaviour
{
    [SerializeField] private SeedDataSO _seedData;
    [SerializeField] private Transform _hologramSpawnPoint;
    [SerializeField] private GameObject _hologramObject;

    public static List<SeedDataSO> _seedDataList = new();
    private PlayerInteractable _playerInteractable;

    private void Awake()
    {
        _playerInteractable = GetComponent<PlayerInteractable>();
    }

    private void OnEnable()
    {
        _playerInteractable.OnInteract += SpawnHologram;
    }

    private void SpawnHologram()
    {
        if (_seedDataList.Contains(_seedData)) return;
        _seedDataList.Add(_seedData);

        var hologram =  Instantiate(_hologramObject, _hologramSpawnPoint.position, Quaternion.identity);
        Destroy(hologram,10f);
    }
}
