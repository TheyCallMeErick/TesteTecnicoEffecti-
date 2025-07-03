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

    [HttpGet("apply-migrations")]
    [EndpointSummary("Aplica as migrations no banco de dados.")]
    [EndpointDescription("Força a criação das tabelas do banco de dados com base nas entidades definidas.")]
    [EndpointName("ApplyMigrations")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public IActionResult ApplyMigrations()
    {
        try
        {
            context.Database.EnsureCreated();
            return Ok(new { message = "Migrations aplicadas com sucesso." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao aplicar as migrations: " + ex.Message });
        }
    }

    [HttpGet]
    [EndpointSummary("Consulta paginada de licitações com filtros.")]
    [EndpointDescription("Retorna uma lista de licitações com base nos filtros de UASG, Pregão, página e tamanho da página.")]
    [EndpointName("QueryLicitacoes")]
    [ProducesResponseType(typeof(PaginatedResultDTO<ResponseLicitacaoDTO>), 200)]
    [ProducesResponseType(400)]
    public IActionResult Query(
       [FromQuery] LicitacaoQuery query
    )
    {
        try
        {

            var licitacoesQuery = context.Licitacoes.AsQueryable();

            if (!string.IsNullOrEmpty(query.Uasg))
            {
                licitacoesQuery = licitacoesQuery.Where(l => l.CodigoUASG.Contains(query.Uasg));
            }

            if (!string.IsNullOrEmpty(query.Pregao))
            {
                licitacoesQuery = licitacoesQuery.Where(l => l.NumeroPregao.Contains(query.Pregao));
            }

            int totalItems = licitacoesQuery.Count();

            var licitacoes = licitacoesQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(l => new ResponseLicitacaoDTO
                {
                    Id = l.Id.ToString(),
                    Pregao = l.NumeroPregao,
                    Uasg = l.CodigoUASG,
                    Fax = l.Fax,
                    Telefone = l.Telefone,
                    Instituicao = l.Instituicao,
                    Orgao = l.Orgao,
                    DataDisponibilizacaoEdital = l.DataDisponibilizacaoEdital,
                    HoraInicioEdital = l.HoraInicioEdital,
                    HoraFimEdital = l.HoraFimEdital,
                    DataEntregaProposta = l.DataEntregaProposta,
                    HoraEntregaProposta = l.HoraEntregaProposta,
                    EnderecoEntrega = l.EnderecoEntrega,
                    Objeto = l.Objeto,
                    Itens = l.Itens.Select(i => new ResponseItemLicitacaoDTO
                    {
                        Id = i.Id.ToString(),
                        Descricao = i.Descricao,
                        Quantidade = i.Quantidade,
                        Nome = i.Nome,
                        UnidadeFornecimento = i.UnidadeFornecimento,
                        Aplicabilidade7174 = i.Aplicabilidade7174,
                        AplicabilidadeMargemPreferencia = i.AplicabilidadeMargemPreferencia,
                        TratamentoDiferenciado = i.TratamentoDiferenciado
                    }).ToList()
                })
                .ToList();

            var result = new PaginatedResultDTO<ResponseLicitacaoDTO>
            {
                TotalItems = totalItems,
                Page = query.Page,
                PageSize = query.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalItems / query.PageSize),
                Data = licitacoes
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Erro ao consultar licitações: " + ex.Message });
        }
    }

    [HttpGet("sync")]
    [EndpointSummary("Sincroniza os dados de licitações com fonte externa.")]
    [EndpointDescription("Importa licitações de fontes externas e salva no banco de dados.")]
    [EndpointName("SincronizarLicitacoes")]
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
