using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
        class MoveTween : Vector3Tween
        {
            readonly Transform transform;

            public MoveTween(Transform transform, Vector3 from, Vector3 to, float time) : base(transform.gameObject, from, to, time)
            {
                this.transform = transform;
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
            
            protected override void OnEasedUpdate(Vector3 value) => transform.position = value;
        }

        public static Tween Move(GameObject gameObject, Vector3 to, float time) => Move(gameObject.transform, gameObject.transform.position, to, time);
        public static Tween Move(GameObject gameObject, Vector3 from, Vector3 to, float time) => Move(gameObject.transform, from, to, time);
        public static Tween Move(Transform transform, Vector3 to, float time) => Move(transform, transform.position, to, time);
        public static Tween Move(Transform transform, Vector3 from, Vector3 to, float time) => AddTween(new MoveTween(transform, from, to, time));
    }
}