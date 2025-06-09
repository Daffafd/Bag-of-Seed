using System;
using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
        class ColorTween : Tween
        {
            readonly Color from;
            readonly Color to;
            Func<float, Color> ease;
            Action<Color> onColorUpdate;

            public ColorTween(Color from, Color to, float time) : base(time)
            {
                this.from = from;
                this.to = to;
                ease = EaseLinear;
            }

            public ColorTween(GameObject gameObject, Color from, Color to, float time) : base(gameObject, time)
            {
                this.from = from;
                this.to = to;
                ease = EaseLinear;
            }

            protected sealed override void OnUpdate(float value)
            {
                Color color = ease(value);
                onColorUpdate?.Invoke(color);
                OnEasedUpdate(color);
            }

            protected virtual void OnEasedUpdate(Color value) { }

            protected override Tween SetOnColorUpdate(Action<Color> callback)
            {
                onColorUpdate += callback;
                return base.SetOnColorUpdate(callback);
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

            Color EaseLinear(float value) => from + (to - from) * value;
            Color EasePunch(float value) => from + (to - from) * _punch.Evaluate(value);
            Color EaseShake(float value) => from + (to - from) * _shake.Evaluate(value);

            Color EaseSpring(float value)
            {
                value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value)
                        * (1f + 1.2f * (1f - value));
                return from + (to - from) * value;
            }

            Color EaseCurve(float value)
            {
                if (animationCurve == null) return EaseLinear(value);
                value = animationCurve.Evaluate(value);
                return from + (to - from) * value;
            }

            Color EaseInSine(float value)
            {
                Color distance = to - from;
                value = -Mathf.Cos(value * Mathf.PI * 0.5f);
                return from + distance * value + distance;
            }

            Color EaseOutSine(float value)
            {
                Color distance = to - from;
                value = -Mathf.Sin(value * Mathf.PI * 0.5f);
                return from + distance * value;
            }

            Color EaseInOutSine(float value)
            {
                Color halfDistance = (to - from) * 0.5f;
                value = -(Mathf.Cos(Mathf.PI * value) - 1f);
                return from + halfDistance * value;
            }

            Color EaseInQuad(float value)
            {
                Color distance = to - from;
                value *= value;
                return distance * value + from;
            }

            Color EaseOutQuad(float value)
            {
                Color distance = to - from;
                value = -value * (value - 2f);
                return distance * value + from;
            }


            Color EaseInOutQuad(float value)
            {
                Color halfDistance = (to - from) * 0.5f;

                value *= 2f;

                if (value < 1f)
                {
                    value *= value;
                    return halfDistance * value + from;
                }

                value = (1f - value) * (value - 3f) + 1f;
                return halfDistance * value + from;
            }

            Color EaseInCubic(float value) => (to - from) * (value * value * value) + from;

            Color EaseOutCubic(float value)
            {
                Color distance = to - from;
                value -= 1f;
                value = value * value * value + 1;
                return distance * value + from;
            }

            Color EaseInOutCubic(float value)
            {
                Color halfDistance = (to - from) * 0.5f;

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

            Color EaseInQuart(float value)
            {
                Color distance = to - from;
                value = value * value * value * value;
                return distance * value + from;
            }

            Color EaseOutQuart(float value)
            {
                Color distance = to - from;
                value -= 1f;
                value = -(value * value * value * value - 1);
                return distance * value + from;
            }


            Color EaseInOutQuart(float value)
            {
                Color halfDistance = (to - from) * 0.5f;

                value *= 2f;
                if (value < 1f)
                {
                    value = value * value * value * value;
                    return halfDistance * value + from;
                }

                value -= 2f;
                return -1 * halfDistance * (value * value * value * value - 2f) + from;
            }

            Color EaseInQuint(float value)
            {
                Color distance = to - from;

                value = value * value * value * value * value;
                return distance * value + from;
            }

            Color EaseOutQuint(float value)
            {
                Color distance = to - from;

                value -= 1f;
                value = value * value * value * value * value + 1f;
                return distance * value + from;
            }

            Color EaseInOutQuint(float value)
            {
                Color halfDistance = (to - from) * 0.5f;

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


            Color EaseInExpo(float value)
            {
                Color distance = to - from;
                value = Mathf.Pow(2f, 10f * (value - 1f));
                return distance * value + from;
            }

            Color EaseOutExpo(float value)
            {
                Color distance = to - from;
                value = -Mathf.Pow(2f, -10f * value) + 1f;
                return distance * value + from;
            }

            Color EaseInOutExpo(float value)
            {
                Color halfDistance = (to - from) * 0.5f;
                value *= 2f;
                if (value < 1) return halfDistance * Mathf.Pow(2, 10 * (value - 1)) + from;
                value--;
                return halfDistance * (-Mathf.Pow(2, -10 * value) + 2) + from;
            }

            Color EaseInCirc(float value)
            {
                Color distance = to - from;
                value = -(Mathf.Sqrt(1f - value * value) - 1f);
                return distance * value + from;
            }

            Color EaseOutCirc(float value)
            {
                Color distance = to - from;
                value -= 1f;
                value = Mathf.Sqrt(1f - value * value);
                return distance * value + from;
            }

            Color EaseInOutCirc(float value)
            {
                Color halfDistance = (to - from) * 0.5f;
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

            Color EaseInBounce(float value)
            {
                Color distance = to - from;
                value = 1f - value;
                return distance - new Color(
                    CalculateEaseOutBounce(0, distance.r, value),
                    CalculateEaseOutBounce(0, distance.g, value),
                    CalculateEaseOutBounce(0, distance.b, value),
                    CalculateEaseOutBounce(0, distance.a, value)
                    ) + from;
            }

            Color EaseOutBounce(float value)
            {
                Color distance = to - from;
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

            Color EaseInOutBounce(float value)
            {
                Color distance = to - from;
                Color halfDistance = distance * 0.5f;
                value *= 2f;
                if (value < 1f)
                    return from + new Color(
                        CalculateEaseInBounce(0, distance.r, value) * 0.5f,
                        CalculateEaseInBounce(0, distance.g, value) * 0.5f,
                        CalculateEaseInBounce(0, distance.b, value) * 0.5f,
                        CalculateEaseInBounce(0, distance.a, value) * 0.5f
                    );
                value -= 1f;
                return new Color(
                    CalculateEaseOutBounce(0, distance.r, value) * 0.5f + halfDistance.r + from.r,
                    CalculateEaseOutBounce(0, distance.g, value) * 0.5f + halfDistance.g + from.g,
                    CalculateEaseOutBounce(0, distance.b, value) * 0.5f + halfDistance.b + from.b,
                    CalculateEaseOutBounce(0, distance.a, value) * 0.5f + halfDistance.a + from.a
                );
            }

            Color EaseInBack(float value)
            {
                Color distance = to - from;
                float overshoot = 1.0f;
                value /= 1;
                float s = 1.70158f * overshoot;
                return distance * (value * value * ((s + 1) * value - s)) + from;
            }

            Color EaseOutBack(float value)
            {
                Color distance = to - from;
                float overshoot = 1.0f;
                float s = 1.70158f * overshoot;
                value = value / 1 - 1;
                value = value * value * ((s + 1) * value + s) + 1;
                return distance * value + from;
            }

            Color EaseInOutBack(float value)
            {
                Color halfDistance = (to - from) * 0.5f;
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

            Color EaseInElastic(float value)
            {
                float overshoot = 1.0f;
                float period = 0.3f;
                return new Color(
                    CalculateEaseInElastic(from.r, to.r, value, overshoot, period),
                    CalculateEaseInElastic(from.g, to.g, value, overshoot, period),
                    CalculateEaseInElastic(from.b, to.b, value, overshoot, period),
                    CalculateEaseInElastic(from.a, to.a, value, overshoot, period)
                );
            }

            Color EaseOutElastic(float value)
            {
                float overshoot = 1.0f;
                float period = 0.3f;
                return new Color(
                    CalculateEaseOutElastic(from.r, to.r, value, overshoot, period),
                    CalculateEaseOutElastic(from.g, to.g, value, overshoot, period),
                    CalculateEaseOutElastic(from.b, to.b, value, overshoot, period),
                    CalculateEaseOutElastic(from.a, to.a, value, overshoot, period)
                );
            }

            Color EaseInOutElastic(float value)
            {
                float overshoot = 1.0f;
                float period = 0.3f;
                return new Color(
                    CalculateEaseInOutElastic(from.r, to.r, value, overshoot, period),
                    CalculateEaseInOutElastic(from.g, to.g, value, overshoot, period),
                    CalculateEaseInOutElastic(from.b, to.b, value, overshoot, period),
                    CalculateEaseInOutElastic(from.a, to.a, value, overshoot, period)
                );
            }
        }

        public static Tween Color(Color from, Color to, float time) => AddTween(new ColorTween(from, to, time));
        public static Tween Color(GameObject gameObject, Color from, Color to, float time) => AddTween(new ColorTween(gameObject, from, to, time));
    }
}