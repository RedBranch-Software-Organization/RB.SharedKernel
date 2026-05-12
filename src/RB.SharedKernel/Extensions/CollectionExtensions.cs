namespace RB.SharedKernel.Extensions;

public static class CollectionExtensions
{
    public static bool IsEmpty<T>(this ICollection<T> collection)
        => collection is not null && collection.Count == 0;

}
