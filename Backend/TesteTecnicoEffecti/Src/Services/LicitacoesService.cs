using System.Threading.Tasks;
using TesteTecnicoEffecti.Src.Data;
using TesteTecnicoEffecti.Src.Facades;

namespace TesteTecnicoEffecti.Src.Services;

public class LicitacoesService : ILicitacaoService
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
        try
        {

            var last = dbContext.Licitacoes.OrderByDescending(a => a.Id).FirstOrDefault();
            var licitacoes = licitacoesFacade.QueryAll(last);
            await dbContext.Licitacoes.AddRangeAsync(licitacoes);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
           Console.WriteLine("Erro ao sincronizar:");
        Console.WriteLine($"Mensagem: {ex.Message}");

        if (ex.InnerException != null)
            Console.WriteLine($"InnerException: {ex.InnerException.Message}");

        if (ex.InnerException?.InnerException != null)
            Console.WriteLine($"InnerException2: {ex.InnerException.InnerException.Message}");
            throw; 
        }
    }
}
