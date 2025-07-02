namespace TesteTecnicoEffecti.Src.DTOs.Responses;

public class ResponseLicitacaoDTO
{
    public string Id { get; set; } = string.Empty;
    public string Uasg { get; set; } = string.Empty;
    public string Pregao { get; set; } = string.Empty;
    public string Objeto { get; set; } = string.Empty;
    public string Orgao { get; set; } = string.Empty;
    public string Instituicao { get; set; } = string.Empty;
    public DateTime DataDisponibilizacaoEdital { get; set; }
    public TimeSpan HoraInicioEdital { get; set; }
    public TimeSpan HoraFimEdital { get; set; }
    public string EnderecoEntrega { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Fax { get; set; } = string.Empty;
    public DateTime DataEntregaProposta { get; set; }
    public TimeSpan HoraEntregaProposta { get; set; }
    public IEnumerable<ResponseItemLicitacaoDTO> Itens { get; set; } = new List<ResponseItemLicitacaoDTO>();
}
