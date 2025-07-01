
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using TesteTecnicoEffecti.Src.DTOs.Outputs;
using TesteTecnicoEffecti.Src.Exceptions;
using TesteTecnicoEffecti.Src.Models;

namespace TesteTecnicoEffecti.Src.Facades;

public class ConsultaLicitacoesFacade : IConsultaLicitacoesFacade
{
    private readonly HtmlWeb web;
    private readonly HtmlDocument document;

    public ConsultaLicitacoesFacade()
    {
        web = new HtmlWeb();
        // document = web.Load("http://comprasnet.gov.br/ConsultaLicitacoes/ConsLicitacaoDia.asp");
        document = web.Load("https://web.archive.org/web/20191218091934/http://comprasnet.gov.br/ConsultaLicitacoes/ConsLicitacaoDia.asp");

    }

    public async Task<LicitacoesPaginationOutputDTO> GetPagination()
    {
        var count = document.DocumentNode.QuerySelector("center");
        if (count != null)
        {
            var data = count.InnerText.Trim();

            return LicitacoesPaginationOutputDTO.FromText(data);
        }

        var errorMessage = document.DocumentNode.QuerySelector(".mensagem");
        if (errorMessage.InnerText == "N�o existe licita��o para o crit�rio informado.")
        {
            throw new NoElementsFoundException();
        }
        throw new Exception();
    }

    public async Task<IEnumerable<Licitacao>> QueryAll(Licitacao? ultimaLicitacaoProcessada)
    {
        var count = await GetPagination();
        List<Licitacao> licitacoes = new();
        for (var i = 0; i < count.TotalPages; i++)
        {
            var document = web.Load("https://web.archive.org/web/20191218091934/http://comprasnet.gov.br/ConsultaLicitacoes/ConsLicitacaoDia.asp");
            var elements = document.DocumentNode.QuerySelectorAll("form");
            var dataElementContainer = elements[0].QuerySelector(".tex3");
            var text = dataElementContainer.InnerText;

            var cleaned = Regex.Replace(text, @"\s+", " ").Trim();

            var uasg = Regex.Match(cleaned, @"Código da UASG: (\d+)").Groups[1].Value;
            var modalidadeNumero = Regex.Match(cleaned, @"Tomada de preço Nº ([\d/]+)").Groups[1].Value;
            var objeto = Regex.Match(cleaned, @"Objeto:\s*Objeto:\s*(.+?) Edital a partir de:").Groups[1].Value.Trim();
            var edital = Regex.Match(cleaned, @"Edital a partir de:\s*(.+?) Endereço:").Groups[1].Value.Trim();
            var endereco = Regex.Match(cleaned, @"Endereço:\s*(.+?) Telefone:").Groups[1].Value.Trim();
            var entrega = Regex.Match(cleaned, @"Entrega da Proposta:\s*(.+?) ").Groups[1].Value.Trim();
            if (
                ultimaLicitacaoProcessada != null &&
                uasg == ultimaLicitacaoProcessada.CodigoUASG &&
                modalidadeNumero == ultimaLicitacaoProcessada.NumeroPregao &&
                objeto == ultimaLicitacaoProcessada.Objeto &&
                endereco == ultimaLicitacaoProcessada.EnderecoEntrega
                )
            {
                break;
            }
            licitacoes.Add(new Licitacao
            {
                CodigoUASG = uasg,
                EnderecoEntrega = endereco,
                Objeto = objeto
            });
        }
        return licitacoes;
    }

}
