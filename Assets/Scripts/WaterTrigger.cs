using System;
using System.Collections;
using System.Collections.Generic;
using Sounds;
using UnityEngine;
using Utility;

public class WaterTrigger : MonoBehaviour
{
   [SerializeField] private Transform _vfxSpawnTransform;
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         VfxManager.Instance.SpawnFromPool("WaterSplash",
            new Vector3(other.transform.position.x, _vfxSpawnTransform.position.y, other.transform.position.z),
            Quaternion.identity, 1f);

         SoundManager.Instance.PlaySfx("WaterSplash", transform.position);
      }
   }
}
