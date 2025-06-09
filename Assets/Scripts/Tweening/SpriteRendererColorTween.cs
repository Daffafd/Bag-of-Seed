using UnityEngine;

namespace Utility.Tweening
{

        public partial class Tween
        {
             class SpriteRendererColorTween: ColorTween
            {
                readonly SpriteRenderer spriteRenderer;

                public SpriteRendererColorTween(SpriteRenderer spriteRenderer, Color from, Color to, float time) : base(
                    spriteRenderer.gameObject, from, to, time)
                {
                    this.spriteRenderer = spriteRenderer;
                }


                 
                protected override bool IsValid()
                {
                    if (spriteRenderer == null)
                    {
                        Debug.LogError("Invalid Color Tween - sprite renderer is null");
                        return false;
                    } 
                    return base.IsValid();
                }


                protected override void OnEasedUpdate(Color value) => spriteRenderer.color = value;

            }
             
             public static Tween Alpha(SpriteRenderer spriteRenderer, Color to, float time) =>
                 AddTween(new SpriteRendererColorTween(spriteRenderer, spriteRenderer.color, to, time));

             public static Tween Alpha(SpriteRenderer spriteRenderer, Color from, Color to, float time) =>
                 AddTween(new SpriteRendererColorTween(spriteRenderer, from, to, time));
        }
    }