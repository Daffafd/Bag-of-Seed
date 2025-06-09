using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEditor;

namespace Utility
{
    public class PooledObject
    {
        public GameObject objectToPool;

        public int size;
    }
    
    [CreateAssetMenu(fileName = "VfxPool", menuName = "Database/VfxPool")]
    [System.Serializable]
    public class VfxPool : SerializedScriptableObject
    {
        [SerializeField]
        private Dictionary <string, PooledObject> pools = new Dictionary<string, PooledObject>();

        public Dictionary<string, PooledObject> Pools
        {
            get => pools;
            set => pools = value;
        }
    }
}