using Microsoft.AspNetCore.Mvc;
using TesteTecnicoEffecti.Src.Data;
using TesteTecnicoEffecti.Src.DTOs.Requests.Query;
using TesteTecnicoEffecti.Src.DTOs.Responses;
using TesteTecnicoEffecti.Src.Services;

namespace TesteTecnicoEffecti.Src.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]

public class LicitacaoController : Controller
{
    private readonly ApplicationDbContext context;
    private readonly ILicitacaoService licitacaoService;

    public LicitacaoController(ApplicationDbContext context, ILicitacaoService licitacaoService)
    {
        this.context = context;
        this.licitacaoService = licitacaoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginationResultDto<ResponseLicitacaoDto>), 200)]
    [ProducesResponseType(400)]
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

    [HttpGet("sync")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Sync()
    {
        try
        {
            await licitacaoService.Sync();
            return Ok(new { message = "Sincronização concluída com sucesso." });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao sincronizar: {ex.StackTrace}");
            return StatusCode(500, new { message = "Erro ao sincronizar: " + ex.Message });
        }
    }

}
