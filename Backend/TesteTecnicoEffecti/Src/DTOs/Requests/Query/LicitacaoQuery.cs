using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace TesteTecnicoEffecti.Src.DTOs.Requests.Query; 

public class LicitacaoQuery
{
    [Description("Código da UASG para filtro")]
    public string? Uasg { get; set; }
    [Description("Número do pregão para filtro")]
    public string? Pregao { get; set; }
    [Description("Número da página (padrão = 1)")]
    public int Page { get; set; } = 1;
    [Description("Tamanho da página (padrão = 10)")]
    public int PageSize { get; set; } = 10;
}
