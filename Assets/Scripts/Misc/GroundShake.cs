using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundShake : MonoBehaviour
{
    [SerializeField] private GameObject _bossPrefab;
    private GameObject _spawnedBoss;

    [SerializeField] private Vector3 _bossOffset;
    public void AnimationComplete()
    {
        if(!_spawnedBoss)
         _spawnedBoss = Instantiate(_bossPrefab,
             transform.position + _bossOffset,
             Quaternion.identity);
        else
        {
            _spawnedBoss.transform.position = transform.position + _bossOffset;
            _spawnedBoss.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
