using System.Text.RegularExpressions;

namespace TesteTecnicoEffecti.Src.DTOs.Outputs;

public class LicitacoesPaginationOutputDTO
{
    public int ActualPage { get; set; }
    public int PageSize { get; set; }
    public int TotalItens { get; set; }
    public int TotalPages { get; set; }
    public int? NextPage { get; set; }

    public static LicitacoesPaginationOutputDTO FromText(string text)
    {
        var match = Regex.Match(text, @"(\d+)\s*-\s*(\d+)\s*de\s*(\d+)");
        if (!match.Success)
        {
            throw new ArgumentException("Texto de paginação inválido.");
        }

        int start = int.Parse(match.Groups[1].Value);
        int end = int.Parse(match.Groups[2].Value);
        int total = int.Parse(match.Groups[3].Value);

        int pageSize = end - start + 1;
        int actualPage = (int)Math.Ceiling((double)end / pageSize);
        int totalPages = (int)Math.Ceiling((double)total / pageSize);

        int? proximaPagina = actualPage < totalPages ? actualPage + 1 : (int?)null;

        return new LicitacoesPaginationOutputDTO
        {
            ActualPage = actualPage,
            PageSize = pageSize,
            TotalItens = total,
            TotalPages = totalPages,
            NextPage = proximaPagina
        };
    }
}
