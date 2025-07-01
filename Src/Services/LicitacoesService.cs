using System.Threading.Tasks;
using TesteTecnicoEffecti.Src.Data;
using TesteTecnicoEffecti.Src.Facades;

namespace TesteTecnicoEffecti.Src.Services;

public class LicitacoesService
{
    private readonly ApplicationDbContext dbContext;
    private readonly IConsultaLicitacoesFacade licitacoesFacade;

    public LicitacoesService(ApplicationDbContext dbContext, IConsultaLicitacoesFacade licitacoesFacade)
    {
        this.dbContext = dbContext;
        this.licitacoesFacade = licitacoesFacade;
    }

    public async Task Sync()
    {
        var last = dbContext.Licitacoes.OrderByDescending(a => a.Id).FirstOrDefault();
        var licitacoes = await licitacoesFacade.QueryAll(last);
        await dbContext.Licitacoes.AddRangeAsync(licitacoes);
        await dbContext.SaveChangesAsync();
    }
}
