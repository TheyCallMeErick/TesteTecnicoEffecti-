namespace TesteTecnicoEffecti.Src.Models;

public class Licitacao
{
    public Guid Id { get; set; }

    public string Orgao { get; set; } = string.Empty;
    public string Universidade { get; set; } = string.Empty;
    public string Instituicao { get; set; } = string.Empty;

    public string CodigoUASG { get; set; } = string.Empty;

    public string NumeroPregao { get; set; } = string.Empty;

    public string Objeto { get; set; } = string.Empty;

    public DateTime DataDisponibilizacaoEdital { get; set; }
    public TimeSpan HoraInicioEdital { get; set; }
    public TimeSpan HoraFimEdital { get; set; }

    public string EnderecoEntrega { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;
    public string Fax { get; set; } = string.Empty;

    public DateTime DataEntregaProposta { get; set; }
    public TimeSpan HoraEntregaProposta { get; set; }
}
