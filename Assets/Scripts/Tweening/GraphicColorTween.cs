using UnityEngine;
using UnityEngine.UI;

namespace Utility.Tweening
{
    public partial class Tween
    {
        class GraphicColorTween : ColorTween
        {
            readonly Graphic graphic;

            public GraphicColorTween(Graphic graphic, Color from, Color to, float time) : base(graphic.gameObject, from, to, time)
            {
                this.graphic = graphic;
            }
            
            protected override bool IsValid()
            {
                if (graphic == null)
                {
                    Debug.LogError("Invalid Color Tween - graphic is null");
                    return false;
                } 
                return base.IsValid();
            }


            protected override void OnEasedUpdate(Color value) => graphic.color = value;
        }

        public static Tween Color(Graphic graphic, Color to, float time) => AddTween(new GraphicColorTween(graphic, graphic.color, to, time));
        public static Tween Color(Graphic graphic, Color from, Color to, float time) => AddTween(new GraphicColorTween(graphic, from, to, time));
    }
}