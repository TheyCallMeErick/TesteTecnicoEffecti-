
using System.Collections;
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
        document = web.Load("http://comprasnet.gov.br/ConsultaLicitacoes/ConsLicitacaoDia.asp");

    }

    public LicitacoesPaginationOutputDTO GetPagination()
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
        var count = GetPagination();
        List<Licitacao> licitacoes = new();
        for (var page = 0; page < count.TotalPages; page++)
        {
            HtmlDocument document = GetDocument(page);
            var elements = document.DocumentNode.QuerySelectorAll(".texField2");
            elements.RemoveAt(elements.Count - 1);
            List<string> urls = getUrls(elements);
            foreach (var url in urls)
            {
                var completeDocument = web.Load(url);
                var dataElements = completeDocument.QuerySelectorAll(".tex3");
                var dataElementContainer = dataElements[0].ParentNode;
                var textElements = dataElements[0].QuerySelectorAll(".tex3");

                var orgao = textElements[1].InnerText.Trim();
                var instituicaoRaw = textElements.Count ==3 ? textElements[2].InnerText : "";
                var instituicao = instituicaoRaw != null ? instituicaoRaw.Trim() : "";
                var uasg = textElements.Last().InnerText.Split(":")[1].Trim();
                var dataElement = document.QuerySelector(".mensagem")?.ParentNode ;
                var text = dataElement?.InnerText ?? "";
                var cleaned = Regex.Replace(text, @"\s+", " ").Trim();

                var numeroPregao = Regex.Match(text, @"Nº\s+(\d+/\d+)\s*-", RegexOptions.IgnoreCase).Groups[1].Value;
                var objeto = Regex.Match(cleaned, @"Objeto:.*Objeto:\s*(.+?)Edital a partir de:").Groups[1].Value.Trim();
                var dataEdital = Regex.Match(cleaned, @"Edital a partir de:\s*(.+?) Endereço:").Groups[1].Value.Trim();
                var endereco = Regex.Match(cleaned, @"Endereço:\s*(.+?)Telefone:").Groups[1].Value.Trim();
                var entrega = Regex.Match(cleaned, @"Entrega da Proposta:\s*(.+?) ").Groups[1].Value.Trim();
                if (
                    ultimaLicitacaoProcessada != null &&
                    uasg == ultimaLicitacaoProcessada.CodigoUASG &&
                    numeroPregao == ultimaLicitacaoProcessada.NumeroPregao &&
                    objeto == ultimaLicitacaoProcessada.Objeto &&
                    endereco == ultimaLicitacaoProcessada.EnderecoEntrega
                    )
                {
                    break;
                }
                var itens = extractItens(dataElements);

                licitacoes.Add(new Licitacao
                {
                    CodigoUASG = uasg,
                    EnderecoEntrega = endereco,
                    Objeto = objeto,
                    Instituicao = instituicao,
                    Orgao = orgao,
                    NumeroPregao = numeroPregao,
                    Itens = itens.ToList(),
                });
            }
        }
        return licitacoes;
    }

    private static IEnumerable<ItemLicitacao> extractItens(IList<HtmlNode> dataElements)
    {
        var itensHtml = dataElements.Where(x => x.InnerText.Contains("Itens de Material")).FirstOrDefault();
        var itens = new List<ItemLicitacao>();
        if (itensHtml != null ){
        var itemData = itensHtml.QuerySelectorAll("table tr");
            foreach (var element in itemData)
            {
                if (element.InnerText == " ")
                {
                    continue;
                }
                var titulo = element.QuerySelector(".tex3b")?.InnerText.Trim();
                if( titulo == null || titulo == "Itens de Material")
                {
                    continue;
                }
                var item = new ItemLicitacao();
                if (titulo == null)
                {
                    continue;
                }
                var tituloParts = titulo.Split("-");
                item.Nome = tituloParts.Length > 1 && tituloParts[1] != null ? tituloParts[1].Trim() : titulo;

                var texto = element.QuerySelector(".tex3")?.InnerText;
                if (texto == null)
                {
                    continue;
                }
                item.Descricao = Regex.Match(texto, @"^(.+?)Tratamento Diferenciado:").Groups[1].Value.Trim();
                item.TratamentoDiferenciado = Regex.Match(texto, @"Tratamento Diferenciado:\s*(.+)Aplicabilidade Decreto").Groups[1].Value.Trim();
                item.Aplicabilidade7174 = Regex.Match(texto, @"Aplicabilidade Decreto 7174:\s*(sim|n[aã]o)", RegexOptions.IgnoreCase).Groups[1].Value.Trim();
                item.AplicabilidadeMargemPreferencia = Regex.Match(texto, @"Aplicabilidade Margem de Preferência:\s*(sim|n[aã]o)", RegexOptions.IgnoreCase).Groups[1].Value.Trim();
                var quantidadeString = Regex.Match(texto, @"Quantidade:\s*(\d+)").Groups[1].Value;
                item.Quantidade = int.Parse(quantidadeString==""? "0" : quantidadeString);
                item.UnidadeFornecimento = Regex.Match(texto, @"Unidade de fornecimento:\s*(.+)").Groups[1].Value.Trim();
                itens.Add(item);

            }
}
        return itens;
    }

    private HtmlDocument GetDocument(int i)
    {
        var mainUrl = "http://comprasnet.gov.br/ConsultaLicitacoes/ConsLicitacaoDia.asp";
        if (i > 0)
        {
            mainUrl += $"?pagina={i + 1}";
        }
        var document = web.Load(mainUrl);
        return document;
    }

    private static List<string> getUrls(IList<HtmlNode> elements)
    {
        List<string> urls = new();
        foreach (var element in elements)
        {
            var onclick = element.GetAttributeValue("onclick", "");
            if (string.IsNullOrEmpty(onclick))
            {
                continue;
            }

            var match = Regex.Match(onclick, @"VisualizarItens\(document\.Form\d+,'\?coduasg=(\d+)&modprp=(\d+)&numprp=([\d/]+)'\);");
            if (!match.Success)
            {
                continue;
            }

            var codUasg = match.Groups[1].Value;
            var modalidade = match.Groups[2].Value;
            var numeroPregao = match.Groups[3].Value;

            var url = $"http://comprasnet.gov.br/ConsultaLicitacoes/download/download_editais_detalhe.asp?coduasg={codUasg}&modprp={modalidade}&numprp={numeroPregao}";
            urls.Add(url);
        }

        return urls;
    }
}
