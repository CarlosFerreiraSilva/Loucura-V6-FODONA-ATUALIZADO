using AngleSharp.Html.Parser;
using CoderCarrer.Domain;
using CoderCarrer.Models;
using HtmlAgilityPack;
using static System.Net.WebRequestMethods;

namespace CoderCarrer.DAL
{
    public class TrabalhaBrasilDAO : IExtratorVaga
    {
        List<Vaga> _lista;
        public List<Vaga> getVagas()
        {
             ExtrairDados().Wait();
            return _lista;
        }

        private async Task<List<Vaga>>  ExtrairDados() {

            var parser = new HtmlParser();
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync("https://www.trabalhabrasil.com.br/vagas-empregos/programador?pagina=1");
            var document = await parser.ParseDocumentAsync(content);
            var doc = new HtmlDocument();
            doc.LoadHtml(document.DocumentElement.OuterHtml);

          
            var vaga = doc.DocumentNode.SelectNodes("//div[contains(@class, 'jg__job')]");
            _lista = new List<Vaga>();
            foreach (var item in vaga)
            {
                Vaga newVaga = new Vaga();
                var htmldocument = new HtmlDocument();
                htmldocument.LoadHtml(item.InnerHtml);

                var titulo = htmldocument.DocumentNode.SelectSingleNode("//h2[contains(@class, 'job__name')]").InnerText;
                var detalhe = htmldocument.DocumentNode.SelectSingleNode("//h3[contains(@class, 'job__detail')]").InnerText;
                var empresa = htmldocument.DocumentNode.SelectSingleNode("//h3[contains(@class, 'job__company')]").InnerText;
                var descricao = htmldocument.DocumentNode.SelectSingleNode("//p[contains(@class, 'job__description')]").InnerText;
                var link = htmldocument.DocumentNode.SelectSingleNode("//a[contains(@class, 'job__vacancy')]").GetAttributeValue("href", "");

                newVaga.titulo = titulo;
                newVaga.empresa = empresa;
                newVaga.descricao_vaga = descricao;
                newVaga.salario = detalhe;
                newVaga.url = "https://www.trabalhabrasil.com.br/" + link;

                _lista.Add(newVaga);    
            }

       
            return _lista;
             

        }


    }
}
;