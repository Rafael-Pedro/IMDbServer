namespace MulviParking.Server.Infra.Database.Abstractions;

public record PaginatedQueryOptions(int Page, int PageSize = 0, bool? IsDescending = null)
{
    public int ItensToSkip => PageSize * (Page - 1);
}