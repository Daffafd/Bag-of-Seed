using System;
using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
        public readonly TweenId id;

        readonly GameObject gameObject;

        float time;
        float elapsed;
        int direction = 1;
        float delay;
        
        Action onComplete;
        Action<float> onUpdate;
        
        AnimationCurve animationCurve;

        TweenLoopType loopType = TweenLoopType.None;
        int loopCount = -1;

        static readonly AnimationCurve _punch = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(0.112586f, 0.9976035f),
            new Keyframe(0.3120486f, -0.1720615f), new Keyframe(0.4316337f, 0.07030682f), new Keyframe(0.5524869f, -0.03141804f),
            new Keyframe(0.6549395f, 0.003909959f), new Keyframe(0.770987f, -0.009817753f), new Keyframe(0.8838775f, 0.001939224f),
            new Keyframe(1.0f, 0.0f));

        static readonly AnimationCurve _shake = new AnimationCurve(new Keyframe(0f, 0f),
            new Keyframe(0.25f, 1f), new Keyframe(0.75f, -1f), new Keyframe(1f, 0f));

        Tween() => id = TweenId.New();
        Tween(float time) : this() => this.time = time;
        Tween(GameObject gameObject, float time) : this(time) => this.gameObject = gameObject;

        public Tween SetEase(AnimationCurve curve)
        {
            SetEase(TweenEaseType.AnimationCurve);
            animationCurve = curve;
            return this;
        }

        public Tween SetOnUpdate(Action<float> callback)
        {
            onUpdate += callback;
            return this;
        }

        protected virtual Tween SetOnVector3Update(Action<Vector3> callback) => this;
        protected virtual Tween SetOnColorUpdate(Action<Color> callback) => this;
        public Tween SetOnUpdate(Action<Vector3> callback) => SetOnVector3Update(callback);
        public Tween SetOnUpdate(Action<Color> callback) => SetOnColorUpdate(callback);
        
        public Tween SetOnComplete(Action callback)
        {
            onComplete += callback;
            return this;
        }

        bool Update()
        {
            if (time <= 0) return true;

            if (delay > 0)
            {
                delay -= Time.deltaTime;
                return false;
            }

            elapsed += Time.deltaTime * direction;

            Mathf.Clamp(elapsed, 0, time);

            float value = Mathf.Clamp01(elapsed / time);

            OnUpdate(value);
            
            bool complete = direction > 0 ? value >= 1.0f : value <= 0;

            if (!complete) return false;

            if (loopType == TweenLoopType.None) return true;

            if (loopCount > 0) loopCount--;

            if (loopCount == 0) return true;

            if (loopType == TweenLoopType.Cycle) elapsed = direction > 0 ? 0 : time;
            if (loopType == TweenLoopType.PingPong) direction = -direction;

            return false;
        }

        public Tween SetDelay(float seconds)
        {
            delay = seconds;
            return this;
        }

        public Tween Loop(int repeats = -1)
        {
            loopType = TweenLoopType.Cycle;
            loopCount = repeats;
            return this;
        }

        public Tween PingPong(int repeats = -1)
        {
            loopType = TweenLoopType.PingPong;
            loopCount = repeats;
            return this;
        }

        public virtual Tween SetEase(TweenEaseType type) => this;
        protected virtual bool IsValid() => true;

        protected virtual void OnUpdate(float value) => onUpdate?.Invoke(value);

        public static void Cancel(TweenId id) => _manager.Remove(id);
        public static void Cancel(GameObject gameObject) => _manager.Remove(gameObject);
        public static void Cancel(Transform transform) => Cancel(transform.gameObject);
        
        public static bool IsTweening(TweenId id) => _manager.Contains(id);

        public static Tween Timer(float time) => AddTween(new Tween(null, time));
        public static Tween Timer(GameObject gameObject, float time) => AddTween(new Tween(gameObject, time));

        public static Tween DelayedCall(float time, Action callback) => DelayedCall(null, time, callback);
        public static Tween DelayedCall(GameObject gameObject, float time, Action callback) => AddTween(new Tween(gameObject, time).SetOnComplete(callback));

        static Tween AddTween(Tween tween)
        {
            _manager.Add(tween);
            return tween;
        }
    }
}