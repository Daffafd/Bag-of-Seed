using System;
using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
        class ScalarTween : Tween
        {
            readonly float from;
            readonly float to;
            Func<float, float> ease;

            public ScalarTween(GameObject gameObject, float from, float to, float time) : base(gameObject, time)
            {
                this.from = from;
                this.to = to;
                ease = EaseLinear;
            }

            public ScalarTween(float from, float to, float time) : base(time)
            {
                this.from = from;
                this.to = to;
                ease = EaseLinear;
            }

            protected sealed override void OnUpdate(float value)
            {
                float scalar = ease(value);
                onUpdate?.Invoke(scalar);
                OnEasedUpdate(scalar);
            }

            protected virtual void OnEasedUpdate(float value) {}
         
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

            float EaseLinear(float value) => from + (to - from) * value;
            float EasePunch(float value) => from + (to - from) * _punch.Evaluate(value);
            float EaseShake(float value) => from + (to - from) * _shake.Evaluate(value);

            float EaseSpring(float value)
            {
                value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value)
                        * (1f + 1.2f * (1f - value));
                return from + (to - from) * value;
            }

            float EaseCurve(float value)
            {
                if (animationCurve == null) return EaseLinear(value);
                value = animationCurve.Evaluate(value);
                return from + (to - from) * value;
            }

            float EaseInSine(float value)
            {
                float distance = to - from;
                value = -Mathf.Cos(value * Mathf.PI * 0.5f);
                return from + distance * value + distance;
            }

            float EaseOutSine(float value)
            {
                float distance = to - from;
                value = -Mathf.Sin(value * Mathf.PI * 0.5f);
                return from + distance * value;
            }

            float EaseInOutSine(float value)
            {
                float halfDistance = (to - from) * 0.5f;
                value = -(Mathf.Cos(Mathf.PI * value) - 1f);
                return from + halfDistance * value;
            }

            float EaseInQuad(float value)
            {
                float distance = to - from;
                value *= value;
                return distance * value + from;
            }

            float EaseOutQuad(float value)
            {
                float distance = to - from;
                value = -value * (value - 2f);
                return distance * value + from;
            }


            float EaseInOutQuad(float value)
            {
                float halfDistance = (to - from) * 0.5f;

                value *= 2f;

                if (value < 1f)
                {
                    value *= value;
                    return halfDistance * value + from;
                }

                value = (1f - value) * (value - 3f) + 1f;
                return halfDistance * value + from;
            }

            float EaseInCubic(float value) => (to - from) * (value * value * value) + from;

            float EaseOutCubic(float value)
            {
                float distance = to - from;
                value -= 1f;
                value = value * value * value + 1;
                return distance * value + from;
            }

            float EaseInOutCubic(float value)
            {
                float halfDistance = (to - from) * 0.5f;

                value *= 2f;

                if (value < 1f)
                {
                    value = value * value * value;
                    return halfDistance * value + from;
                }

                value -= 2f;
                value = value * value * value + 2f;
                return halfDistance * value + from;
            }

            float EaseInQuart(float value)
            {
                float distance = to - from;
                value = value * value * value * value;
                return distance * value + from;
            }

            float EaseOutQuart(float value)
            {
                float distance = to - from;
                value -= 1f;
                value = -(value * value * value * value - 1);
                return distance * value + from;
            }


            float EaseInOutQuart(float value)
            {
                float halfDistance = (to - from) * 0.5f;

                value *= 2f;
                if (value < 1f)
                {
                    value = value * value * value * value;
                    return halfDistance * value + from;
                }

                value -= 2f;
                return -halfDistance * (value * value * value * value - 2f) + from;
            }


            float EaseInQuint(float value)
            {
                float distance = to - from;

                value = value * value * value * value * value;
                return distance * value + from;
            }

            float EaseOutQuint(float value)
            {
                float distance = to - from;

                value -= 1f;
                value = value * value * value * value * value + 1f;
                return distance * value + from;
            }

            float EaseInOutQuint(float value)
            {
                float halfDistance = (to - from) * 0.5f;

                value *= 2f;
                if (value < 1f)
                {
                    value = value * value * value * value * value;
                    return halfDistance * value + from;
                }

                value -= 2f;
                value = value * value * value * value * value + 2f;
                return halfDistance * value + from;
            }


            float EaseInExpo(float value)
            {
                float distance = to - from;
                value = Mathf.Pow(2f, 10f * (value - 1f));
                return distance * value + from;
            }

            float EaseOutExpo(float value)
            {
                float distance = to - from;
                value = -Mathf.Pow(2f, -10f * value) + 1f;
                return distance * value + from;
            }

            float EaseInOutExpo(float value)
            {
                float halfDistance = (to - from) * 0.5f;
                value *= 2f;
                if (value < 1) return halfDistance * Mathf.Pow(2, 10 * (value - 1)) + from;
                value--;
                return halfDistance * (-Mathf.Pow(2, -10 * value) + 2) + from;
            }

            float EaseInCirc(float value)
            {
                float distance = to - from;
                value = -(Mathf.Sqrt(1f - value * value) - 1f);
                return distance * value + from;
            }

            float EaseOutCirc(float value)
            {
                float distance = to - from;
                value -= 1f;
                value = Mathf.Sqrt(1f - value * value);
                return distance * value + from;
            }

            float EaseInOutCirc(float value)
            {
                float halfDistance = (to - from) * 0.5f;
                value *= 2f;
                if (value < 1f)
                {
                    value = -(Mathf.Sqrt(1f - value * value) - 1f);
                    return halfDistance * value + from;
                }

                value -= 2f;
                value = Mathf.Sqrt(1f - value * value) + 1f;
                return halfDistance * value + from;
            }

            float EaseInBounce(float value)
            {
                float distance = to - from;
                value = 1f - value;
                return distance - CalculateEaseOutBounce(0, distance, value) + from;
            }

            float EaseOutBounce(float value)
            {
                float distance = to - from;
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

            float EaseInOutBounce(float value)
            {
                float distance = to - from;
                float halfDistance = distance * 0.5f;
                value *= 2f;
                if (value < 1f) return CalculateEaseInBounce(0, distance, value) * 0.5f + from;
                value -= 1f;
                return CalculateEaseOutBounce(0, distance, value) * 0.5f + halfDistance + @from;
            }

            float EaseInBack(float value)
            {
                float distance = to - from;
                float overshoot = 1.0f;
                value /= 1;
                float s = 1.70158f * overshoot;
                return distance * (value * value * ((s + 1) * value - s)) + from;
            }

            float EaseOutBack(float value)
            {
                float distance = to - from;
                float overshoot = 1.0f;
                float s = 1.70158f * overshoot;
                value = value / 1 - 1;
                value = value * value * ((s + 1) * value + s) + 1;
                return distance * value + from;
            }

            float EaseInOutBack(float value)
            {
                float halfDistance = (to - from) * 0.5f;
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

            float EaseInElastic(float value)
            {
                float overshoot = 1.0f;
                float period = 0.3f;
                return CalculateEaseInElastic(from, to, value, overshoot, period);
            }

            float EaseOutElastic(float value)
            {
                float overshoot = 1.0f;
                float period = 0.3f;
                return CalculateEaseOutElastic(from, to, value, overshoot, period);
            }

            float EaseInOutElastic(float value)
            {
                float overshoot = 1.0f;
                float period = 0.3f;
                return CalculateEaseInOutElastic(from, to, value, overshoot, period);
            }
        }

        public static Tween Scalar(float from, float to, float time) => AddTween(new ScalarTween(from, to, time));
        public static Tween Scalar(GameObject gameObject, float from, float to, float time) => AddTween(new ScalarTween(gameObject, from, to, time));
    }
}