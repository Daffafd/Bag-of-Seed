using Sirenix.OdinInspector;
using UnityEngine;

namespace Interactables
{
    [CreateAssetMenu(menuName = "SeedData")]
    public class SeedDataSO : SerializedScriptableObject
    {
        public RuntimeAnimatorController AnimatorController;
        public GameObject ObjectToInstantiate;
        public GameObject SeedPrefab;
        public GameObject vfxHoldingSeed;
        public string GrowSfxId;
    }
}