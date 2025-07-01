using Microsoft.AspNetCore.Mvc;
using TesteTecnicoEffecti.Src.Data;
using TesteTecnicoEffecti.Src.DTOs.Requests.Query;
using TesteTecnicoEffecti.Src.DTOs.Responses;

namespace TesteTecnicoEffecti.Src.Controllers;

[ApiController]
public class LicitacaoController : Controller
{
    private readonly ApplicationDbContext context;

    public LicitacaoController(ApplicationDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public IActionResult Query(
       [FromQuery] LicitacaoQuery query
    )
    {
        var licitacoesQuery = context.Licitacoes.AsQueryable();

        if (!string.IsNullOrEmpty(query.Uasg))
        {
            licitacoesQuery = licitacoesQuery.Where(l => l.CodigoUASG == query.Uasg);
        }

        if (!string.IsNullOrEmpty(query.Pregao))
        {
            licitacoesQuery = licitacoesQuery.Where(l => l.NumeroPregao == query.Pregao);
        }

        int totalItems = licitacoesQuery.Count();

        var licitacoes = licitacoesQuery
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(l => new ResponseLicitacaoDto
            {
                Id = l.Id.ToString(),
                Pregao = l.NumeroPregao,
                Uasg = l.CodigoUASG
            })
            .ToList();

        var result = new PaginationResultDto<ResponseLicitacaoDto>
        {
            TotalItems = totalItems,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalPages = (int)Math.Ceiling((double)totalItems / query.PageSize),
            Items = licitacoes
        };

        return Ok(result);
    }
}
