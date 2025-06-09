using System;

namespace Utility.Tweening
{
    public readonly struct TweenId : IEquatable<TweenId>, IComparable<TweenId>
    {
        readonly Guid value;

        TweenId(Guid value) => this.value = value;

        public static TweenId Empty => new TweenId(Guid.Empty);

        public static TweenId New() => new TweenId(Guid.NewGuid());

        public int CompareTo(TweenId other) => value.CompareTo(other.value);

        public bool Equals(TweenId other) => value.Equals(other.value);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is TweenId other && Equals(other);
        }

        public override int GetHashCode() => value.GetHashCode();
        public override string ToString() => value.ToString();

        public static bool operator ==(TweenId a, TweenId b) => a.Equals(b);
        public static bool operator !=(TweenId a, TweenId b) => !(a == b);
    }
}