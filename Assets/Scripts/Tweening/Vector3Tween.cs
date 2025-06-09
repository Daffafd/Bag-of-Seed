using System;
using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
        class Vector3Tween : Tween
        {
            readonly Vector3 from;
            readonly Vector3 to;
            Func<float, Vector3> ease;
            
            Action<Vector3> onVector3Update;

            protected Vector3Tween(GameObject gameObject, Vector3 from, Vector3 to, float time) : base(gameObject, time)
            {
                this.from = from;
                this.to = to;
                this.time = time;
                ease = EaseLinear;
            }

            protected Vector3Tween(Vector3 from, Vector3 to, float time) : base(time)
            {
                this.from = from;
                this.to = to;
                this.time = time;
                ease = EaseLinear;
            }

            protected sealed override void OnUpdate(float value)
            {
                Vector3 vector3 = ease(value);
                onVector3Update?.Invoke(vector3);
                OnEasedUpdate(vector3);
            }

            protected virtual void OnEasedUpdate(Vector3 value) { }

            protected override Tween SetOnVector3Update(Action<Vector3> callback)
            {
                onVector3Update += callback;
                return base.SetOnVector3Update(callback);
            }
            
            public override Tween SetEase(TweenEaseType type)
            {
                ease = type switch
                {
                    TweenEaseType.None => EaseLinear,
                    TweenEaseType.Linear => EaseLinear,
                    TweenEaseType.Punch => EasePunch,
                    TweenEaseType.Shake => EaseShake,
                    TweenEaseType.Spring => EaseSpring,
                    TweenEaseType.AnimationCurve => EaseCurve,
                    TweenEaseType.InSine => EaseInSine,
                    TweenEaseType.OutSine => EaseOutSine,
                    TweenEaseType.InOutSine => EaseInOutSine,
                    TweenEaseType.InQuad => EaseInQuad,
                    TweenEaseType.OutQuad => EaseOutQuad,
                    TweenEaseType.InOutQuad => EaseInOutQuad,
                    TweenEaseType.InCubic => EaseInCubic,
                    TweenEaseType.OutCubic => EaseOutCubic,
                    TweenEaseType.InOutCubic => EaseInOutCubic,
                    TweenEaseType.InQuart => EaseInQuart,
                    TweenEaseType.OutQuart => EaseOutQuart,
                    TweenEaseType.InOutQuart => EaseInOutQuart,
                    TweenEaseType.InQuint => EaseInQuint,
                    TweenEaseType.OutQuint => EaseOutQuint,
                    TweenEaseType.InOutQuint => EaseInOutQuint,
                    TweenEaseType.InExpo => EaseInExpo,
                    TweenEaseType.OutExpo => EaseOutExpo,
                    TweenEaseType.InOutExpo => EaseInOutExpo,
                    TweenEaseType.InCirc => EaseInCirc,
                    TweenEaseType.OutCirc => EaseOutCirc,
                    TweenEaseType.InOutCirc => EaseInOutCirc,
                    TweenEaseType.InBounce => EaseInBounce,
                    TweenEaseType.OutBounce => EaseOutBounce,
                    TweenEaseType.InOutBounce => EaseInOutBounce,
                    TweenEaseType.InBack => EaseInBack,
                    TweenEaseType.OutBack => EaseOutBack,
                    TweenEaseType.InOutBack => EaseInOutBack,
                    TweenEaseType.InElastic => EaseInElastic,
                    TweenEaseType.OutElastic => EaseOutElastic,
                    TweenEaseType.InOutElastic => EaseInOutElastic,
                    _ => EaseLinear
                };
                return base.SetEase(type);
            }

            Vector3 EaseLinear(float value)
            {
                Vector3 distance = to - from;
                return new Vector3(from.x + distance.x * value, from.y + distance.y * value, from.z + distance.z * value);
            }

            Vector3 EasePunch(float value)
            {
                Vector3 distance = to - from;
                value = _punch.Evaluate(value);
                return new Vector3(from.x + distance.x * value, from.y + distance.y * value, from.z + distance.z * value);
            }

            Vector3 EaseShake(float value)
            {
                Vector3 distance = to - from;
                value = _shake.Evaluate(value);
                return new Vector3(from.x + distance.x * value, from.y + distance.y * value, from.z + distance.z * value);
            }

            Vector3 EaseSpring(float value)
            {
                Vector3 distance = to - from;
                value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) *
                        (1f + 1.2f * (1f - value));
                return from + distance * value;
            }

            Vector3 EaseCurve(float value)
            {
                if (animationCurve == null) return EaseLinear(value);
                Vector3 distance = to - from;
                value = animationCurve.Evaluate(value);
                return new Vector3(from.x + distance.x * value, from.y + distance.y * value, from.z + distance.z * value);
            }

            Vector3 EaseInSine(float value)
            {
                Vector3 distance = to - from;
                value = -Mathf.Cos(value * Mathf.PI * 0.5f);
                return new Vector3(distance.x * value + distance.x + from.x, distance.y * value + distance.y + from.y,
                    distance.z * value + distance.z + from.z);
            }

            Vector3 EaseOutSine(float value)
            {
                Vector3 distance = to - from;
                value = -Mathf.Sin(value * Mathf.PI * 0.5f);
                return new Vector3(distance.x * value + from.x, distance.y * value + from.y, distance.z * value + from.z);
            }

            Vector3 EaseInOutSine(float value)
            {
                Vector3 halfDistance = (to - from) * 0.5f;
                value = -(Mathf.Cos(Mathf.PI * value) - 1f);
                return new Vector3(halfDistance.x * value + from.x, halfDistance.y * value + from.y, halfDistance.z * value + from.z);
            }

            Vector3 EaseInQuad(float value)
            {
                Vector3 distance = to - from;
                value *= value;
                return new Vector3(distance.x * value + from.x, distance.y * value + from.y, distance.z * value + from.z);
            }

            Vector3 EaseOutQuad(float value)
            {
                Vector3 distance = to - from;
                value = -value * (value - 2f);
                return distance * value + @from;
            }


            Vector3 EaseInOutQuad(float value)
            {
                Vector3 halfDistance = (to - from) * 0.5f;

                value *= 2f;

                if (value < 1f)
                {
                    value *= value;
                    return new Vector3(halfDistance.x * value + from.x, halfDistance.y * value + from.y, halfDistance.z * value + from.z);
                }

                value = (1f - value) * (value - 3f) + 1f;
                return new Vector3(halfDistance.x * value + from.x, halfDistance.y * value + from.y, halfDistance.z * value + from.z);
            }

            Vector3 EaseInCubic(float value)
            {
                Vector3 distance = to - from;
                value = value * value * value;
                return new Vector3(distance.x * value + from.x, distance.y * value + from.y, distance.z * value + from.z);
            }

            Vector3 EaseOutCubic(float value)
            {
                Vector3 distance = to - from;
                value -= 1f;
                value = value * value * value + 1;
                return new Vector3(distance.x * value + from.x, distance.y * value + from.y, distance.z * value + from.z);
            }

            Vector3 EaseInOutCubic(float value)
            {
                Vector3 halfDistance = (to - from) * 0.5f;

                value *= 2f;

                if (value < 1f)
                {
                    value = value * value * value;
                    return new Vector3(halfDistance.x * value + from.x, halfDistance.y * value + from.y, halfDistance.z * value + from.z);
                }

                value -= 2f;
                value = value * value * value + 2f;
                return new Vector3(halfDistance.x * value + from.x, halfDistance.y * value + from.y, halfDistance.z * value + from.z);
            }

            Vector3 EaseInQuart(float value)
            {
                Vector3 distance = to - from;
                value = value * value * value * value;
                return distance * value + from;
            }

            Vector3 EaseOutQuart(float value)
            {
                Vector3 distance = to - from;
                value -= 1f;
                value = -(value * value * value * value - 1);
                return new Vector3(distance.x * value + from.x, distance.y * value + from.y, distance.z * value + from.z);
            }


            Vector3 EaseInOutQuart(float value)
            {
                Vector3 halfDistance = (to - from) * 0.5f;

                value *= 2f;
                if (value < 1f)
                {
                    value = value * value * value * value;
                    return new Vector3(halfDistance.x * value + from.x, halfDistance.y * value + from.y, halfDistance.z * value + from.z);
                }

                value -= 2f;
                return -halfDistance * (value * value * value * value - 2f) + from;
            }


            Vector3 EaseInQuint(float value)
            {
                Vector3 distance = to - from;

                value = value * value * value * value * value;
                return new Vector3(distance.x * value + from.x, distance.y * value + from.y, distance.z * value + from.z);
            }

            Vector3 EaseOutQuint(float value)
            {
                Vector3 distance = to - from;

                value -= 1f;
                value = value * value * value * value * value + 1f;
                return new Vector3(distance.x * value + from.x, distance.y * value + from.y, distance.z * value + from.z);
            }

            Vector3 EaseInOutQuint(float value)
            {
                Vector3 halfDistance = (to - from) * 0.5f;

                value *= 2f;
                if (value < 1f)
                {
                    value = value * value * value * value * value;
                    return new Vector3(halfDistance.x * value + from.x, halfDistance.y * value + from.y, halfDistance.z * value + from.z);
                }

                value -= 2f;
                value = value * value * value * value * value + 2f;
                return new Vector3(halfDistance.x * value + from.x, halfDistance.y * value + from.y, halfDistance.z * value + from.z);
            }


            Vector3 EaseInExpo(float value)
            {
                Vector3 distance = to - from;
                value = Mathf.Pow(2f, 10f * (value - 1f));
                return new Vector3(distance.x * value + from.x, distance.y * value + from.y, distance.z * value + from.z);
            }

            Vector3 EaseOutExpo(float value)
            {
                Vector3 distance = to - from;
                value = -Mathf.Pow(2f, -10f * value) + 1f;
                return new Vector3(distance.x * value + from.x, distance.y * value + from.y, distance.z * value + from.z);
            }

            Vector3 EaseInOutExpo(float value)
            {
                Vector3 halfDistance = (to - from) * 0.5f;
                value *= 2f;
                if (value < 1) return halfDistance * Mathf.Pow(2, 10 * (value - 1)) + from;
                value--;
                return halfDistance * (-Mathf.Pow(2, -10 * value) + 2) + from;
            }

            Vector3 EaseInCirc(float value)
            {
                Vector3 distance = to - from;
                value = -(Mathf.Sqrt(1f - value * value) - 1f);
                return new Vector3(distance.x * value + from.x, distance.y * value + from.y, distance.z * value + from.z);
            }

            Vector3 EaseOutCirc(float value)
            {
                Vector3 distance = to - from;
                value -= 1f;
                value = Mathf.Sqrt(1f - value * value);

                return new Vector3(distance.x * value + from.x, distance.y * value + from.y, distance.z * value + from.z);
            }

            Vector3 EaseInOutCirc(float value)
            {
                Vector3 halfDistance = (to - from) * 0.5f;
                value *= 2f;
                if (value < 1f)
                {
                    value = -(Mathf.Sqrt(1f - value * value) - 1f);
                    return new Vector3(halfDistance.x * value + from.x, halfDistance.y * value + from.y, halfDistance.z * value + from.z);
                }

                value -= 2f;
                value = Mathf.Sqrt(1f - value * value) + 1f;
                return new Vector3(halfDistance.x * value + from.x, halfDistance.y * value + from.y, halfDistance.z * value + from.z);
            }

            Vector3 EaseInBounce(float value)
            {
                Vector3 distance = to - from;
                value = 1f - value;
                return new Vector3(distance.x - CalculateEaseOutBounce(0, distance.x, value) + from.x,
                    distance.y - CalculateEaseOutBounce(0, distance.y, value) + from.y,
                    distance.z - CalculateEaseOutBounce(0, distance.z, value) + from.z);
            }

            Vector3 EaseOutBounce(float value)
            {
                Vector3 distance = to - from;
                float overshoot = 1.0f;
                float valM, valN; // bounce values
                if (value < (valM = 1 - 1.75f * overshoot / 2.75f))
                {
                    value = 1 / valM / valM * value * value;
                }
                else if (value < (valN = 1 - .75f * overshoot / 2.75f))
                {
                    value -= (valM + valN) / 2;
                    // first bounce, height: 1/4
                    value = 7.5625f * value * value + 1 - .25f * overshoot * overshoot;
                }
                else if (value < (valM = 1 - .25f * overshoot / 2.75f))
                {
                    value -= (valM + valN) / 2;
                    // second bounce, height: 1/16
                    value = 7.5625f * value * value + 1 - .0625f * overshoot * overshoot;
                }
                else
                {
                    // valN = 1
                    value -= (valM + 1) / 2;
                    // third bounce, height: 1/64
                    value = 7.5625f * value * value + 1 - .015625f * overshoot * overshoot;
                }

                return distance * value + from;
            }

            Vector3 EaseInOutBounce(float value)
            {
                Vector3 distance = to - from;
                Vector3 halfDistance = distance * 0.5f;

                value *= 2f;
                if (value < 1f)
                {
                    return new Vector3(CalculateEaseInBounce(0, distance.x, value) * 0.5f + from.x,
                        CalculateEaseInBounce(0, distance.y, value) * 0.5f + from.y,
                        CalculateEaseInBounce(0, distance.z, value) * 0.5f + from.z);
                }
                else
                {
                    value -= 1f;
                    return new Vector3(CalculateEaseOutBounce(0, distance.x, value) * 0.5f + halfDistance.x + from.x,
                        CalculateEaseOutBounce(0, distance.y, value) * 0.5f + halfDistance.y + from.y,
                        CalculateEaseOutBounce(0, distance.z, value) * 0.5f + halfDistance.z + from.z);
                }
            }

            Vector3 EaseInBack(float value)
            {
                Vector3 distance = to - from;

                float overshoot = 1.0f;
                value /= 1;
                float s = 1.70158f * overshoot;
                return distance * (value * value * ((s + 1) * value - s)) + from;
            }

            Vector3 EaseOutBack(float value)
            {
                Vector3 distance = to - from;

                float overshoot = 1.0f;
                float s = 1.70158f * overshoot;
                value = value / 1 - 1;
                value = value * value * ((s + 1) * value + s) + 1;
                return distance * value + from;
            }

            Vector3 EaseInOutBack(float value)
            {
                Vector3 halfDistance = (to - from) * 0.5f;

                float overshoot = 1.0f;
                float s = 1.70158f * overshoot;
                value *= 2f;
                if (value < 1)
                {
                    s *= 1.525f * overshoot;
                    return halfDistance * (value * value * ((s + 1) * value - s)) + from;
                }

                value -= 2;
                s *= 1.525f * overshoot;
                value = value * value * ((s + 1) * value + s) + 2;
                return halfDistance * value + from;
            }

            Vector3 EaseInElastic(float value)
            {
                float overshoot = 1.0f;
                float period = 0.3f;
                return new Vector3(CalculateEaseInElastic(from.x, to.x, value, overshoot, period),
                    CalculateEaseInElastic(from.y, to.y, value, overshoot, period),
                    CalculateEaseInElastic(from.z, to.z, value, overshoot, period));
            }

            Vector3 EaseOutElastic(float value)
            {
                float overshoot = 1.0f;
                float period = 0.3f;
                return new Vector3(CalculateEaseOutElastic(from.x, to.x, value, overshoot, period),
                    CalculateEaseOutElastic(from.y, to.y, value, overshoot, period),
                    CalculateEaseOutElastic(from.z, to.z, value, overshoot, period));
            }

            Vector3 EaseInOutElastic(float value)
            {
                float overshoot = 1.0f;
                float period = 0.3f;
                return new Vector3(CalculateEaseInOutElastic(from.x, to.x, value, overshoot, period),
                    CalculateEaseInOutElastic(from.y, to.y, value, overshoot, period),
                    CalculateEaseInOutElastic(from.z, to.z, value, overshoot, period));
            }
        }
    }
}