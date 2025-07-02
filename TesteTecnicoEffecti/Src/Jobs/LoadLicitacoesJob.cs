using TesteTecnicoEffecti.Src.Services;

namespace TesteTecnicoEffecti.Src.Jobs; 

public class LoadLicitacoesJob : IHostedService, IDisposable
{
    private readonly ILogger<LoadLicitacoesJob> _logger;
    private readonly LicitacoesService licitacoesService;

    private Timer? _timer;

    public LoadLicitacoesJob(ILogger<LoadLicitacoesJob> logger, LicitacoesService licitacoesService)
    {
        _logger = logger;
        this.licitacoesService = licitacoesService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Serviço iniciado.");
        _timer = new Timer(ExecuteTask, null, TimeSpan.Zero, TimeSpan.FromHours(6));

        return Task.CompletedTask;
    }

    private void ExecuteTask(object? state)
    {
        _logger.LogInformation($"Executando tarefa periódica: {DateTime.Now}");
        licitacoesService.Sync().RunSynchronously();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Serviço parado.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
