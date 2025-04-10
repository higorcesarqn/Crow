#pragma warning disable IDE0060 // Remove unused parameter
namespace Crow.Contracts;

public readonly struct Unit : IEquatable<Unit>, IComparable<Unit>, IComparable
{
    public static readonly Unit Value = new();

    public static readonly Task<Unit> Task = System.Threading.Tasks.Task.FromResult(Value);

    public int CompareTo(Unit other)
    {
        return 0;
    }

    int IComparable.CompareTo(object? obj) => 0;

    public override int GetHashCode() => 0;

    public bool Equals(Unit other) => true;

    public override bool Equals(object? obj)
    {
        return obj is Unit;
    }

    public static bool operator ==(Unit? first, Unit? second) => true;

    public static bool operator !=(Unit? first, Unit? second) => false;

    public override string ToString() => "()";
}