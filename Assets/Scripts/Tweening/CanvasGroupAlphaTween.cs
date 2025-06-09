using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
        class CanvasGroupAlphaTween : ScalarTween
        {
            readonly CanvasGroup canvasGroup;
        
            public CanvasGroupAlphaTween(CanvasGroup canvasGroup, float from, float to, float time) : base(canvasGroup.gameObject, from, to, time)
            {
                this.canvasGroup = canvasGroup;
            }

            protected override bool IsValid()
            {
                if (canvasGroup == null)
                {
                    Debug.LogError("Invalid tween - CanvasGroup is null");
                    return false;
                } 
                return base.IsValid();
            }
            
            protected override void OnEasedUpdate(float value) => canvasGroup.alpha = value;
        }

        public static Tween Alpha(CanvasGroup canvasGroup, float to, float time)
        {
            return AddTween(new CanvasGroupAlphaTween(canvasGroup, canvasGroup.alpha, to, time));
        }

        public static Tween Alpha(CanvasGroup canvasGroup, float from, float to, float time)
        {
            return AddTween(new CanvasGroupAlphaTween(canvasGroup, from, to, time));
        }
    }
}