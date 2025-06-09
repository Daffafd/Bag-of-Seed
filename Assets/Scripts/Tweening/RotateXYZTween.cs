using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
        class RotateXYZTween : ScalarTween
        {
            readonly Transform transform;
            readonly VectorFlags flags;

            public RotateXYZTween(Transform transform, VectorFlags flags, float from, float to, float time) : base(transform.gameObject, from, to, time)
            {
                this.transform = transform;
                this.flags = flags;
            }

            protected override bool IsValid()
            {
                if (transform == null)
                {
                    Debug.LogError("Invalid Rotate Tween - transform is null");
                    return false;
                } 
                return base.IsValid();
            }

            protected override void OnEasedUpdate(float value)
            {
                Vector3 eulerAngles = transform.eulerAngles;
                if ((flags & VectorFlags.X) != 0) eulerAngles.x = value;
                if ((flags & VectorFlags.Y) != 0) eulerAngles.y = value;
                if ((flags & VectorFlags.Z) != 0) eulerAngles.z = value;
                transform.rotation = Quaternion.Euler(eulerAngles);
            }
        }

        public static Tween RotateX(Transform transform, float to, float time) => RotateX(transform, transform.eulerAngles.x, to, time);

        public static Tween RotateX(Transform transform, float from, float to, float time) =>
            AddTween(new RotateXYZTween(transform, VectorFlags.X, from, to, time));

        public static Tween RotateY(Transform transform, float to, float time) => RotateY(transform, transform.eulerAngles.y, to, time);

        public static Tween RotateY(Transform transform, float from, float to, float time) =>
            AddTween(new RotateXYZTween(transform, VectorFlags.Y, from, to, time));

        public static Tween RotateZ(Transform transform, float to, float time) => RotateZ(transform, transform.eulerAngles.z, to, time);

        public static Tween RotateZ(Transform transform, float from, float to, float time) =>
            AddTween(new RotateXYZTween(transform, VectorFlags.Z, from, to, time));
    }
}