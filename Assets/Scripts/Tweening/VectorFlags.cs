using System;

namespace Utility.Tweening
{
    public partial class Tween
    {
        [Flags]
        enum VectorFlags
        {
            None = 0,
            X = 1,
            Y = 2,
            Z = 4,
            All = X | Y | Z
        }
    }
}