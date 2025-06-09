using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
        class ScaleTween : Vector3Tween
        {
            readonly Transform transform;

            public ScaleTween(Transform transform, Vector3 from, Vector3 to, float time) : base(from, to, time)
            {
                this.transform = transform;
            }
            
            protected override bool IsValid()
            {
                if (transform == null)
                {
                    Debug.LogError("Invalid Scale Tween - transform is null");
                    return false;
                } 
                return base.IsValid();
            }
            
            protected override void OnEasedUpdate(Vector3 value) => transform.localScale =value;
        }

        public static Tween Scale(GameObject gameObject, Vector3 to, float time) => Scale(gameObject.transform, to, time);
        public static Tween Scale(GameObject gameObject, Vector3 from, Vector3 to, float time) => Scale(gameObject.transform, from, to, time);
        public static Tween Scale(Transform transform, Vector3 to, float time) => Scale(transform, transform.localScale, to, time);
        public static Tween Scale(Transform transform, Vector3 from, Vector3 to, float time) => AddTween(new ScaleTween(transform, from, to, time));
    }
}