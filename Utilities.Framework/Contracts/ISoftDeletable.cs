namespace Utilities.Framework.Contracts
{
    public interface ISoftDeletable<TUserId>
        where TUserId : struct, IComparable, IComparable<TUserId>
    {
        DateTime? DeletedAt { get; }
        TUserId? Deleter { get; }
    }
}
