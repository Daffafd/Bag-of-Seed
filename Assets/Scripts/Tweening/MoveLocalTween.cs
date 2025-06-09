using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
        class MoveLocalTween : Vector3Tween
        {
            readonly Transform transform;

            public MoveLocalTween(Transform transform, Vector3 from, Vector3 to, float time) : base(transform.gameObject, from, to, time)
            {
                this.transform = transform;
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
            
            protected override void OnEasedUpdate(Vector3 value) => transform.localPosition = value;
        }

        public static Tween MoveLocal(GameObject gameObject, Vector3 to, float time) => Move(gameObject.transform, to, time);
        public static Tween MoveLocal(GameObject gameObject, Vector3 from, Vector3 to, float time) => Move(gameObject.transform, from, to, time);
        public static Tween MoveLocal(Transform transform, Vector3 to, float time) => Move(transform, transform.localPosition, to, time);
        public static Tween MoveLocal(Transform transform, Vector3 from, Vector3 to, float time) => AddTween(new MoveLocalTween(transform, from, to, time));
    }
}