using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
        class FollowTween : Tween
        {
            readonly Transform transform;
            readonly Transform target;

            public FollowTween(Transform transform, Transform target, float time) : base(transform.gameObject, time)
            {
                this.transform = transform;
                this.target = target;
            }
            
            protected override bool IsValid()
            {
                if (transform == null)
                {
                    Debug.LogError("Invalid Follow Tween - transform is null");
                    return false;
                } 
                if (target == null)
                {
                    Debug.LogError("Invalid Follow Tween - target is null");
                    return false;
                }
                return base.IsValid();
            }

            protected override void OnUpdate(float value)
            {
                transform.position = target.position;
                base.OnUpdate(value);
            }
        }

        public static Tween Follow(GameObject gameObject, Transform target, float time) => AddTween(new FollowTween(gameObject.transform, target, time));
        public static Tween Follow(Transform transform, Transform target, float time) => AddTween(new FollowTween(transform, target, time));
    }
}