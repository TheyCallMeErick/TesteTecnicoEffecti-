namespace TesteTecnicoEffecti.Src.DTOs.Requests.Query; 

public class LicitacaoQuery
{
    public string? Uasg { get; set; }
    public string? Pregao { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
