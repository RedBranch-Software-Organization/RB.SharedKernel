namespace RB.SharedKernel;

[Serializable]
public abstract class ValueObject : IComparable, IComparable<ValueObject>
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var valueObject = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    public override int GetHashCode()
        => GetEqualityComponents()
            .Aggregate(1, (current, obj) => HashCode.Combine(current, obj));

    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;
        if (GetType() != obj.GetType()) return 1;

        var other = (ValueObject)obj;

        var thisComponents = GetEqualityComponents().GetEnumerator();
        var otherComponents = other.GetEqualityComponents().GetEnumerator();

        while (thisComponents.MoveNext() && otherComponents.MoveNext())
        {
            var comparison = Comparer<object>.Default.Compare(thisComponents.Current, otherComponents.Current);
            if (comparison != 0)
                return comparison;
        }

        return 0;
    }

    public int CompareTo(ValueObject? other)
        => CompareTo(other as object);

    public static bool operator ==(ValueObject a, ValueObject b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(ValueObject a, ValueObject b)
        => !(a == b);

    public static bool operator <(ValueObject left, ValueObject right)
        => left is null ? right is not null : left.CompareTo(right) < 0;

    public static bool operator <=(ValueObject left, ValueObject right)
        => left is null || left.CompareTo(right) <= 0;

    public static bool operator >(ValueObject left, ValueObject right)
        => left is not null && left.CompareTo(right) > 0;

    public static bool operator >=(ValueObject left, ValueObject right)
         => left is null ? right is null : left.CompareTo(right) >= 0;
}
