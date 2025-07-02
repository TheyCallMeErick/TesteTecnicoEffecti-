namespace TesteTecnicoEffecti.Src.DTOs.Responses; 

public class PaginationResultDto<T>
{
    public int TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public List<T> Items { get; set; } = new();
}
