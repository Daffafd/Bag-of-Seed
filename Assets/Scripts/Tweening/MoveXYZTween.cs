using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
        class MoveXYZTween : ScalarTween
        {
            readonly Transform transform;
            readonly VectorFlags flags;
        
            public MoveXYZTween(Transform transform, VectorFlags flags, float from, float to, float time) : base(transform.gameObject, from, to, time)
            {
                this.transform = transform;
                this.flags = flags;
            }
            
            protected override bool IsValid()
            {
                if (transform == null)
                {
                    Debug.LogError("Invalid Move Tween - transform is null");
                    return false;
                } 
                return base.IsValid();
            }

            protected override void OnEasedUpdate(float value)
            {
                Vector3 position = transform.position;
                if ((flags & VectorFlags.X) != 0) position.x = value;
                if ((flags & VectorFlags.Y) != 0) position.y = value;
                if ((flags & VectorFlags.Z) != 0) position.z = value;
                transform.position = position;
            }
        }
        
        public static Tween MoveX(Transform transform, float to, float time) => MoveX(transform, transform.position.x, to, time);
        public static Tween MoveX(Transform transform, float from, float to, float time) => AddTween(new MoveXYZTween(transform, VectorFlags.X, from, to, time));
        
        public static Tween MoveY(Transform transform, float to, float time) => MoveY(transform, transform.position.y, to, time);
        public static Tween MoveY(Transform transform, float from, float to, float time) => AddTween(new MoveXYZTween(transform, VectorFlags.Y, from, to, time));
        
        public static Tween MoveZ(Transform transform, float to, float time) => MoveZ(transform, transform.position.z, to, time);
        public static Tween MoveZ(Transform transform, float from, float to, float time) => AddTween(new MoveXYZTween(transform, VectorFlags.Z, from, to, time));
    }
}