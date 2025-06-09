using UnityEngine;
using UnityEngine.UI;

namespace Utility.Tweening
{
    public partial class Tween
    {
        class GraphicAlphaTween : ScalarTween
        {
            readonly Graphic graphic;

            public GraphicAlphaTween(Graphic graphic, float from, float to, float time) : base(graphic.gameObject, from, to, time)
            {
                this.graphic = graphic;
            }


            protected override bool IsValid()
            {
                if (graphic == null)
                {
                    Debug.LogError("Invalid Alpha Tween - graphic is null");
                    return false;
                } 
                return base.IsValid();
            }

            protected override void OnEasedUpdate(float value)
            {
                Color color = graphic.color;
                color.a = value;
                graphic.color = color;
            }
        }

        public static Tween Alpha(Graphic graphic, float to, float time) => AddTween(new GraphicAlphaTween(graphic, graphic.color.a, to, time));
        public static Tween Alpha(Graphic graphic, float from, float to, float time) => AddTween(new GraphicAlphaTween(graphic, from, to, time));
    }
}