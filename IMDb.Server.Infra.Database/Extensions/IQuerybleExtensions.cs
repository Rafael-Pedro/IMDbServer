using IMDb.Server.Infra.Database.Abstraction;

namespace IMDb.Server.Infra.Database.Extensions;
public static class IQuerybleExtensions
{
    public static IEnumerable<TSource> PaginateAndOrder<TSource, TOrderTarget>(this IEnumerable<TSource> queryble,
    PaginatedQueryOptions paginatedQueryOptions, Func<TSource, TOrderTarget> orderFunction)
    {
        var paginated = paginatedQueryOptions.IsDescending switch
        {
            true => queryble.OrderByDescending(orderFunction),
            false => queryble.OrderBy(orderFunction),
            _ => queryble
        };

        return paginated.Skip(paginatedQueryOptions.ItensToSkip).Take(paginatedQueryOptions.PageSize);
    }
}
