using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
        
        public Tween SetEaseLinear() => SetEase(TweenEaseType.Linear);
        public Tween SetEasePunch() => SetEase(TweenEaseType.Punch);
        public Tween SetEaseShake() => SetEase(TweenEaseType.Shake);
        public Tween SetEaseSpring() => SetEase(TweenEaseType.Spring);
        public Tween SetEaseInSine() => SetEase(TweenEaseType.InSine);
        public Tween SetEaseOutSine() => SetEase(TweenEaseType.OutSine);
        public Tween SetEaseInOutSine() => SetEase(TweenEaseType.InOutSine);
        public Tween SetEaseInQuad() => SetEase(TweenEaseType.InQuad);
        public Tween SetEaseOutQuad() => SetEase(TweenEaseType.OutQuad);
        public Tween SetEaseInOutQuad() => SetEase(TweenEaseType.InOutQuad);
        public Tween SetEase() => SetEase(TweenEaseType.Linear);
        public Tween SetEaseInCubic() => SetEase(TweenEaseType.InCubic);
        public Tween SetEaseOutCubic() => SetEase(TweenEaseType.OutCubic);
        public Tween SetEaseInOutCubic() => SetEase(TweenEaseType.InOutCubic);
        public Tween SetEaseInQuart() => SetEase(TweenEaseType.InQuart);
        public Tween SetEaseOutQuart() => SetEase(TweenEaseType.OutQuart);
        public Tween SetEaseInOutQuart() => SetEase(TweenEaseType.InOutQuart);
        public Tween SetEaseInQuint() => SetEase(TweenEaseType.InQuint);
        public Tween SetEaseOutQuint() => SetEase(TweenEaseType.OutQuint);
        public Tween SetEaseInOutQuint() => SetEase(TweenEaseType.InOutQuint);
        public Tween SetEaseInExpo() => SetEase(TweenEaseType.InExpo);
        public Tween SetEaseOutExpo() => SetEase(TweenEaseType.OutExpo);
        public Tween SetEaseInOutExpo() => SetEase(TweenEaseType.InOutExpo);
        public Tween SetEaseInCirc() => SetEase(TweenEaseType.InCirc);
        public Tween SetEaseOutCirc() => SetEase(TweenEaseType.OutCirc);
        public Tween SetEaseInOutCirc() => SetEase(TweenEaseType.InOutCirc);
        public Tween SetEaseInBounce() => SetEase(TweenEaseType.InBounce);
        public Tween SetEaseOutBounce() => SetEase(TweenEaseType.OutBounce);
        public Tween SetEaseInOutBounce() => SetEase(TweenEaseType.InOutBounce);
        public Tween SetEaseInBack() => SetEase(TweenEaseType.InBack);
        public Tween SetEaseOutBack() => SetEase(TweenEaseType.OutBack);
        public Tween SetEaseInOutBack() => SetEase(TweenEaseType.InOutBack);
        public Tween SetEaseInElastic() => SetEase(TweenEaseType.InElastic);
        public Tween SetEaseOutElastic() => SetEase(TweenEaseType.OutElastic);
        public Tween SetEaseInOutElastic() => SetEase(TweenEaseType.InOutElastic);
                
        static float CalculateEaseInBounce(float start, float end, float val)
            {
                end -= start;
                const float D = 1f;
                return end - CalculateEaseOutBounce(0, end, D - val) + start;
            }

            static float CalculateEaseOutBounce(float start, float end, float val)
            {
                val /= 1f;
                end -= start;
                if (val < 1 / 2.75f)
                {
                    return end * (7.5625f * val * val) + start;
                }

                if (val < 2 / 2.75f)
                {
                    val -= 1.5f / 2.75f;
                    return end * (7.5625f * val * val + .75f) + start;
                }

                if (val < 2.5 / 2.75)
                {
                    val -= 2.25f / 2.75f;
                    return end * (7.5625f * val * val + .9375f) + start;
                }

                val -= 2.625f / 2.75f;
                return end * (7.5625f * val * val + .984375f) + start;
            }

            static float CalculateEaseInOutBounce(float start, float end, float val)
            {
                end -= start;
                const float D = 1f;
                if (val < D / 2) return CalculateEaseInBounce(0, end, val * 2) * 0.5f + start;
                return CalculateEaseOutBounce(0, end, val * 2 - D) * 0.5f + end * 0.5f + start;
            }

            static float CalculateEaseInElastic(float start, float end, float val, float overshoot = 1.0f, float period = 0.3f)
            {
                end -= start;

                float p = period;
                float s = 0f;
                float a = 0f;

                if (val == 0f) return start;

                if (val == 1f) return start + end;

                if (a == 0f || a < Mathf.Abs(end))
                {
                    a = end;
                    s = p / 4f;
                }
                else
                {
                    s = p / (2f * Mathf.PI) * Mathf.Asin(end / a);
                }

                if (overshoot > 1f && val > 0.6f)
                    overshoot = 1f + (1f - val) / 0.4f * (overshoot - 1f);
                // Debug.Log("ease in elastic val:"+val+" a:"+a+" overshoot:"+overshoot);

                val -= 1f;
                return start - a * Mathf.Pow(2f, 10f * val) * Mathf.Sin((val - s) * (2f * Mathf.PI) / p) * overshoot;
            }

            static float CalculateEaseOutElastic(float start, float end, float val, float overshoot = 1.0f, float period = 0.3f)
            {
                end -= start;

                float p = period;
                float s = 0f;
                float a = 0f;

                if (val == 0f) return start;

                // Debug.Log("ease out elastic val:"+val+" a:"+a);
                if (val == 1f) return start + end;

                if (a == 0f || a < Mathf.Abs(end))
                {
                    a = end;
                    s = p / 4f;
                }
                else
                {
                    s = p / (2f * Mathf.PI) * Mathf.Asin(end / a);
                }

                if (overshoot > 1f && val < 0.4f)
                    overshoot = 1f + val / 0.4f * (overshoot - 1f);
                // Debug.Log("ease out elastic val:"+val+" a:"+a+" overshoot:"+overshoot);

                return start + end + a * Mathf.Pow(2f, -10f * val) * Mathf.Sin((val - s) * (2f * Mathf.PI) / p) * overshoot;
            }

            static float CalculateEaseInOutElastic(float start, float end, float val, float overshoot = 1.0f, float period = 0.3f)
            {
                end -= start;

                float p = period;
                float s = 0f;
                float a = 0f;

                if (val == 0f) return start;

                val /= (1f / 2f);
                if (val == 2f) return start + end;

                if (a == 0f || a < Mathf.Abs(end))
                {
                    a = end;
                    s = p / 4f;
                }
                else
                {
                    s = p / (2f * Mathf.PI) * Mathf.Asin(end / a);
                }

                if (overshoot > 1f)
                {
                    if (val < 0.2f)
                    {
                        overshoot = 1f + val / 0.2f * (overshoot - 1f);
                    }
                    else if (val > 0.8f)
                    {
                        overshoot = 1f + (1f - val) / 0.2f * (overshoot - 1f);
                    }
                }

                if (val < 1f)
                {
                    val -= 1f;
                    return start - 0.5f * (a * Mathf.Pow(2f, 10f * val) * Mathf.Sin((val - s) * (2f * Mathf.PI) / p)) * overshoot;
                }

                val -= 1f;
                return end + start + a * Mathf.Pow(2f, -10f * val) * Mathf.Sin((val - s) * (2f * Mathf.PI) / p) * 0.5f * overshoot;
            }
    }
}