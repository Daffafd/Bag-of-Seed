using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
        class ScaleXYZTween : ScalarTween
        {
            readonly Transform transform;
            readonly VectorFlags flags;

            public ScaleXYZTween(Transform transform, VectorFlags flags, float from, float to, float time) : base(transform.gameObject, from, to,
                time)
            {
                this.transform = transform;
                this.flags = flags;
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

            protected override void OnEasedUpdate(float value)
            {
                Vector3 scale = transform.localScale;
                if ((flags & VectorFlags.X) != 0) scale.x = value;
                if ((flags & VectorFlags.Y) != 0) scale.y = value;
                if ((flags & VectorFlags.Z) != 0) scale.z = value;
                transform.localScale = scale;
            }
        }

        public static Tween ScaleX(Transform transform, float to, float time) => ScaleX(transform, transform.localScale.x, to, time);

        public static Tween ScaleX(Transform transform, float from, float to, float time) =>
            AddTween(new ScaleXYZTween(transform, VectorFlags.X, from, to, time));

        public static Tween ScaleY(Transform transform, float to, float time) => ScaleY(transform, transform.localScale.y, to, time);

        public static Tween ScaleY(Transform transform, float from, float to, float time) =>
            AddTween(new ScaleXYZTween(transform, VectorFlags.Y, from, to, time));

        public static Tween ScaleZ(Transform transform, float to, float time) => ScaleZ(transform, transform.localScale.z, to, time);

        public static Tween ScaleZ(Transform transform, float from, float to, float time) =>
            AddTween(new ScaleXYZTween(transform, VectorFlags.Z, from, to, time));
    }
}