namespace IMDb.Server.Infra.Database.Abstraction;

public record PaginatedQueryOptions(int Page, int PageSize = 0, bool? IsDescending = null)
{
    public int ItensToSkip => PageSize * (Page - 1);
}