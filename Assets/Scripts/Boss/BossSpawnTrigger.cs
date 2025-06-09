using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnTrigger : MonoBehaviour
{
    [SerializeField] private WormManager _manager;
    [SerializeField] private Transform _spawnPos;

    [SerializeField] private float _spawnLastLogYPos;
    // Start is called before the first frame update

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _manager.SpawnBossPos = new Vector3(transform.position.x, _spawnPos.localPosition.y,_spawnPos.localPosition.z) ;
            
        }

        if (other.TryGetComponent(out WormBoss boss))
        {
            _manager.SpawnLastLogYPos = _spawnLastLogYPos;
        }
    }
}
