namespace TesteTecnicoEffecti.Src.Models; 

public class ItemLicitacao
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty; 
    public string Descricao { get; set; } = string.Empty; 
    public string TratamentoDiferenciado { get; set; } = string.Empty;
    public string Aplicabilidade7174 { get; set; } = string.Empty;
    public string AplicabilidadeMargemPreferencia { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public string UnidadeFornecimento { get; set; } = string.Empty;
    public Guid LicitacaoId { get; set; }
    public Licitacao? Licitacao { get; set; }
}
