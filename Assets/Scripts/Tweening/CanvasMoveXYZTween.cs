using UnityEngine;

namespace Utility.Tweening
{
    public partial class Tween
    {
        class CanvasMoveXYZTween : ScalarTween
        {
            readonly RectTransform rectTransform;
            readonly VectorFlags flags;
        
            public CanvasMoveXYZTween(RectTransform rectTransform, VectorFlags flags, float from, float to, float time) : base(rectTransform.gameObject, from, to, time)
            {
                this.rectTransform = rectTransform;
                this.flags = flags;
            }
            
            protected override bool IsValid()
            {
                if (rectTransform == null)
                {
                    Debug.LogError("Invalid Move Tween - transform is null");
                    return false;
                } 
                return base.IsValid();
            }

            protected override void OnEasedUpdate(float value)
            {
                Vector3 anchoredPosition3D = rectTransform.anchoredPosition3D;
                if ((flags & VectorFlags.X) != 0) anchoredPosition3D.x = value;
                if ((flags & VectorFlags.Y) != 0) anchoredPosition3D.y = value;
                if ((flags & VectorFlags.Z) != 0) anchoredPosition3D.z = value;
                rectTransform.anchoredPosition3D = anchoredPosition3D;
            }
        }
        
        public static Tween MoveX(RectTransform transform, float to, float time) => MoveX(transform, transform.anchoredPosition3D.x, to, time);
        public static Tween MoveX(RectTransform transform, float from, float to, float time) => AddTween(new CanvasMoveXYZTween(transform, VectorFlags.X, from, to, time));
        
        public static Tween MoveY(RectTransform transform, float to, float time) => MoveY(transform, transform.anchoredPosition3D.y, to, time);
        public static Tween MoveY(RectTransform transform, float from, float to, float time) => AddTween(new CanvasMoveXYZTween(transform, VectorFlags.Y, from, to, time));
        
        public static Tween MoveZ(RectTransform transform, float to, float time) => MoveZ(transform, transform.anchoredPosition3D.z, to, time);
        public static Tween MoveZ(RectTransform transform, float from, float to, float time) => AddTween(new CanvasMoveXYZTween(transform, VectorFlags.Z, from, to, time));
    }
}