using System.Collections.Generic;
using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
        static Manager _manager;

        public static void Init()
        {
            if (_manager != null) return;
            GameObject go = new GameObject("Tween Manager") {hideFlags = HideFlags.HideInHierarchy};
            _manager = go.AddComponent<Manager>();
            Object.DontDestroyOnLoad(go);
        }
        
        [DisallowMultipleComponent]
        class Manager : MonoBehaviour
        {
            readonly List<Tween> tweens = new List<Tween>();
            readonly Dictionary<TweenId, Tween> tweenIds = new Dictionary<TweenId, Tween>();
            readonly Queue<Tween> tweensToRemove = new Queue<Tween>();

            public void Add(Tween tween)
            {
                tweens.Add(tween);
                tweenIds[tween.id] = tween;
            }

            public bool TryGet(TweenId id, out Tween tween) => tweenIds.TryGetValue(id, out tween);

            public void Remove(GameObject go)
            {
                for (int i = tweens.Count; i-- > 0;)
                {
                    Tween tween = tweens[i];
                    if (tween.gameObject != go) continue;
                    tweensToRemove.Enqueue(tween);
                }
            }

            public void Remove(TweenId id)
            {
                if (id == TweenId.Empty) return;
                if (!TryGet(id, out Tween tween)) return;
                tweensToRemove.Enqueue(tween);
            }

            public bool Contains(TweenId id) => tweenIds.ContainsKey(id);

            void LateUpdate()
            {
                while (tweensToRemove.Count > 0)
                {
                    Tween tween = tweensToRemove.Dequeue();
                    tweens.Remove(tween);
                    tweenIds.Remove(tween.id);
                }

                for (int i = tweens.Count; i-- > 0;)
                {
                    Tween tween = tweens[i];

                    if (tweensToRemove.Contains(tween)) continue;
                    
                    if (!tween.IsValid())
                    {
                        tweensToRemove.Enqueue(tween);
                        continue;
                    }
                    bool complete = tween.Update();
                    if (!complete) continue;
                    tweensToRemove.Enqueue(tween);
                    tween.onComplete?.Invoke();
                }
            }
        }
    }
}