using System.Collections.Generic;
using UnityEngine;
using Utility.Tweening;

namespace Utility
{
  public class VfxManager : MonoBehaviour
    {
        public static VfxManager Instance { get; set; }

        [SerializeField] private VfxPool _vfxPool;
        
        private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();


        private Dictionary<GameObject, TweenId> followTweens = new Dictionary<GameObject, TweenId>();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                
                return;
            }
            else
            {
                Instance = this;
            }

            Tween.Init();
            
            poolDictionary.Clear();
            
            foreach (var item in _vfxPool.Pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < item.Value.size; i++)
                {
                    GameObject obj = Instantiate(item.Value.objectToPool);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(item.Key, objectPool);
            }
        }

        public GameObject SpawnWithoutPool(string vfxName, Vector3 position, Quaternion rotation)
        {
            var objectToPool = _vfxPool.Pools[vfxName].objectToPool;

            var spawnedVfx = Instantiate(objectToPool, position, rotation);
            spawnedVfx.SetActive(true);

            return spawnedVfx;
        }


        public GameObject SpawnFromPool(string id, Vector3 position, Quaternion rotation, float duration = -1,
            float scale = -1)
        {

            if (id == "") return null;
            
            if (!poolDictionary.ContainsKey(id))
            {
                Debug.LogWarning("Pool with tag " + id + " doesn't exist.");
                return null;
            }

            GameObject objectToSpawn;
            if (poolDictionary[id].Count > 0)
            {
                objectToSpawn = poolDictionary[id].Dequeue();
            }
            else
            {
                objectToSpawn = Instantiate(_vfxPool.Pools[id].objectToPool);
            }
            
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            
            if (duration != -1)
            {
                Tween.DelayedCall(duration, () => ReturnToPool(id, objectToSpawn));
            }

            if (scale != -1)
            {
                objectToSpawn.transform.localScale = new Vector3(scale, scale, scale);
            }
        
            
            return objectToSpawn;
        }
        
        public GameObject SpawnFromPool(string id, float duration = -1,
            float scale = -1)
        {

            if (id == "") return null;
            
            if (!poolDictionary.ContainsKey(id))
            {
                Debug.LogWarning("Pool with tag " + id + " doesn't exist.");
                return null;
            }

            GameObject objectToSpawn;
            if (poolDictionary[id].Count > 0)
            {
                objectToSpawn = poolDictionary[id].Dequeue();
            }
            else
            {
                objectToSpawn = Instantiate(_vfxPool.Pools[id].objectToPool);
            }
            
            objectToSpawn.SetActive(true);

            if (duration != -1)
            {
                Tween.DelayedCall(duration, () => ReturnToPool(id, objectToSpawn));
            }
            if (scale != -1)
            {
                objectToSpawn.transform.localScale = new Vector3(scale, scale, scale);
            }

            return objectToSpawn;
        }
        
        public GameObject SpawnFromPool(string id, Vector3 position, float duration = -1, float scale = -1)
        {

            if (id == "") return null;
            
            if (!poolDictionary.ContainsKey(id))
            {
                Debug.LogWarning("Pool with tag " + id + " doesn't exist.");
                return null;
            }

            GameObject objectToSpawn;
            if (poolDictionary[id].Count > 0)
            {
                objectToSpawn = poolDictionary[id].Dequeue();
            }
            else
            {
                objectToSpawn = Instantiate(_vfxPool.Pools[id].objectToPool);
            }
            
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;

            if (duration != -1)
            {
                Tween.DelayedCall(duration, () => ReturnToPool(id, objectToSpawn));
            }
            if (scale != -1)
            {
                objectToSpawn.transform.localScale = new Vector3(scale, scale, scale);
            }

            return objectToSpawn;
        }
        
        public GameObject SpawnFromPool (string id, Vector3 position, Quaternion rotation, Transform followTarget, float duration = -1, float scale = -1)
        {
            if (!poolDictionary.ContainsKey(id))
            {
                Debug.LogWarning("Pool with tag " + id + " doesn't exist.");
                return null;
            }

            GameObject objectToSpawn;
            if (poolDictionary[id].Count > 0)
            {
                objectToSpawn = poolDictionary[id].Dequeue();
            }
            else
            {
                objectToSpawn = Instantiate(_vfxPool.Pools[id].objectToPool);
            }
            
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            
            if (duration != -1)
            {
                Tween.DelayedCall(duration, () => ReturnToPool(id, objectToSpawn));
            }

           var tweenId = Tween.Follow(objectToSpawn, followTarget, Mathf.Infinity).id;
           
           followTweens.Add(objectToSpawn, tweenId);
           
           if (scale != -1)
           {
               objectToSpawn.transform.localScale = new Vector3(scale, scale, scale);
           }

            return objectToSpawn;
        }
        
        public GameObject SpawnFromPool (string id, Vector3 position, Transform followTarget, float duration = -1, float scale = -1)
        {
            if (!poolDictionary.ContainsKey(id))
            {
                Debug.LogWarning("Pool with tag " + id + " doesn't exist.");
                return null;
            }

            GameObject objectToSpawn;
            if (poolDictionary[id].Count > 0)
            {
                objectToSpawn = poolDictionary[id].Dequeue();
            }
            else
            {
                objectToSpawn = Instantiate(_vfxPool.Pools[id].objectToPool);
            }
            
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;

            if (duration != -1)
            {
                Tween.DelayedCall(duration, () => ReturnToPool(id, objectToSpawn));
            }

            var tweenId = Tween.Follow(objectToSpawn, followTarget, Mathf.Infinity).id;
           
            followTweens.Add(objectToSpawn, tweenId);
           
            if (scale != -1)
            {
                objectToSpawn.transform.localScale = new Vector3(scale, scale, scale);
            }

            return objectToSpawn;
        }

        public void ReturnToPool(string id, GameObject pooledObject)
        {
            if(pooledObject == null)  return;
            
            pooledObject.SetActive(false);
            poolDictionary[id].Enqueue(pooledObject);

            //pooledObject.transform.parent = null;
            
            pooledObject.transform.SetParent(null);
            
            if (followTweens.ContainsKey(pooledObject))
            {
                Tween.Cancel(followTweens[pooledObject]);

                followTweens.Remove(pooledObject);
            }
        }
    
    }
}