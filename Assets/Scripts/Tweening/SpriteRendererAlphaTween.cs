using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
         class SpriteRendererAlphaTween : ScalarTween
        {
            readonly SpriteRenderer spriteRenderer;

            public SpriteRendererAlphaTween(SpriteRenderer spriteRenderer, float from, float to, float time) : base(
                spriteRenderer.gameObject, from, to, time)
            {
                this.spriteRenderer = spriteRenderer;
            }


            protected override bool IsValid()
            {
                if (spriteRenderer == null)
                {
                    Debug.LogError("Invalid Alpha Tween - sprite renderer is null");
                    return false;
                }

                return base.IsValid();
            }

            protected override void OnEasedUpdate(float value)
            {
                Color color = spriteRenderer.color;
                color.a = value;
                spriteRenderer.color = color;
            }

         
        }
         public static Tween Alpha(SpriteRenderer spriteRenderer, float to, float time) =>
             AddTween(new SpriteRendererAlphaTween(spriteRenderer, spriteRenderer.color.a, to, time));

         public static Tween Alpha(SpriteRenderer spriteRenderer, float from, float to, float time) =>
             AddTween(new SpriteRendererAlphaTween(spriteRenderer, from, to, time));
    }
}