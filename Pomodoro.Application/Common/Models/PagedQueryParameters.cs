namespace Pomodoro.Application.Common.Models;

public record PagedQueryParameters
{
    private const int MaxPageSize = 100;
    private int _pagesize = 10;
    private int _pageNumber = 1;

    public int PageNumber
    {
        get => _pageNumber;
        init => _pageNumber = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pagesize;
        init => _pagesize = value > MaxPageSize ? MaxPageSize : value < 1 ? 1 : value;
    }
    
    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;
}