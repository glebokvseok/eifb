namespace Extensions;

public static class CollectionExtensions
{
    public static bool IsNullOrEmpty<TEntity>(this IEnumerable<TEntity>? collection) =>
        collection is null || !collection.Any();
}