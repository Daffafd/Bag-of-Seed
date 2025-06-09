using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
        class MoveLocalXYZTween : ScalarTween
        {
            readonly Transform transform;
            readonly VectorFlags flags;

            public MoveLocalXYZTween(Transform transform, VectorFlags flags, float from, float to, float time) : base(transform.gameObject, from, to,
                time)
            {
                this.transform = transform;
                this.flags = flags;
            }

            protected override bool IsValid()
            {
                if (transform == null)
                {
                    Debug.LogError("Invalid Move Local Tween - transform is null");
                    return false;
                } 
                return base.IsValid();
            }

            protected override void OnEasedUpdate(float value)
            {
                Vector3 localPosition = transform.localPosition;
                if ((flags & VectorFlags.X) != 0) localPosition.x = value;
                if ((flags & VectorFlags.Y) != 0) localPosition.y = value;
                if ((flags & VectorFlags.Z) != 0) localPosition.z = value;
                transform.localPosition = localPosition;
            }
        }

        public static Tween MoveLocalX(Transform transform, float to, float time) => MoveLocalX(transform, transform.localPosition.x, to, time);

        public static Tween MoveLocalX(Transform transform, float from, float to, float time) =>
            AddTween(new MoveLocalXYZTween(transform, VectorFlags.X, from, to, time));

        public static Tween MoveLocalY(Transform transform, float to, float time) => MoveLocalY(transform, transform.localPosition.y, to, time);

        public static Tween MoveLocalY(Transform transform, float from, float to, float time) =>
            AddTween(new MoveLocalXYZTween(transform, VectorFlags.Y, from, to, time));

        public static Tween MoveLocalZ(Transform transform, float to, float time) => MoveLocalZ(transform, transform.localPosition.z, to, time);

        public static Tween MoveLocalZ(Transform transform, float from, float to, float time) =>
            AddTween(new MoveLocalXYZTween(transform, VectorFlags.Z, from, to, time));
    }
}