using TesteTecnicoEffecti.Src.DTOs.Outputs;
using TesteTecnicoEffecti.Src.Models;

namespace TesteTecnicoEffecti.Src.Facades;

public interface IConsultaLicitacoesFacade
{
    public Task<IEnumerable<Licitacao>> QueryAll(Licitacao? ultimaLicitacaoProcessada);
    public LicitacoesPaginationOutputDTO GetPagination();
}