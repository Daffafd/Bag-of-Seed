using System;
using Interactables;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerVFX : MonoBehaviour
    {
        private SeedDataSO CurrentSeedData { get; set; }

        [SerializeField]
        private GameObject currentVfx;

        [SerializeField] private Vector3 _offSet = new(0,- 1.6f,0);

        public void UpdateSeedData(SeedDataSO newData)
        {
            Destroy(currentVfx);
            currentVfx = null;
            CurrentSeedData = newData;
            if (!newData || (CurrentSeedData && Equals(newData.name, CurrentSeedData.name) && currentVfx)) return;
            currentVfx = SpawnVFX(CurrentSeedData.vfxHoldingSeed);
        }
        
        public GameObject SpawnVFX(GameObject vfx)
        {
            return vfx == null ? null : Instantiate(vfx, transform.position+_offSet, transform.rotation, transform);
        }
    }
}
