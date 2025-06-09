using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu(fileName = "SfxPool", menuName = "Database/SfxPool")]
    public class SfxPool : SerializedScriptableObject
    {
        [SerializeField]
        private Dictionary <string, AudioClip[]> _pools = new Dictionary<string, AudioClip[]>();

        public Dictionary<string, AudioClip[]> Pools
        {
            get => _pools;
            set => _pools = value;
        }

        public AudioClip GetFromPool(string id)
        {
            if(_pools.TryGetValue(id, out AudioClip[] clips))
            {
                return clips[Random.Range(0, clips.Length)];
            }
            else
            {
                Debug.Log("AudioClip not found");
            }

            return null;
        }
    }
}