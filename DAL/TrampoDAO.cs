using AngleSharp.Html.Parser;
using CoderCarrer.Domain;
using CoderCarrer.Models;
using HtmlAgilityPack;

namespace CoderCarrer.DAL
{
    public class TrampoDAO : IExtratorVaga
    {
        List<Vaga> _lista;
        public List<Vaga> getVagas()
        {
            ExtrairDados().Wait();
            return _lista;
        }

        private async Task<List<Vaga>> ExtrairDados()
        {

            var parser = new HtmlParser();
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync("https://www.linkedin.com/jobs/search/?currentJobId=3562678051&geoId=106057199&keywords=Programador&location=Brasil&refresh=true");
            var document = await parser.ParseDocumentAsync(content);
            var doc = new HtmlDocument();
            doc.LoadHtml(document.DocumentElement.OuterHtml);


            var vaga = doc.DocumentNode.SelectNodes("//div[contains(@class, 'result-ite')]");
            _lista = new List<Vaga>();
            foreach (var item in vaga)
            {
                Vaga newVaga = new Vaga();
                var htmldocument = new HtmlDocument();
                htmldocument.LoadHtml(item.InnerHtml);

                var titulo = htmldocument.DocumentNode.SelectSingleNode("//div[contains(@class, 'result-item__title  result-item__title--no-date')]").InnerText;
                var detalhe = htmldocument.DocumentNode.SelectSingleNode("//span[contains(@class, 'result-item__location-label')]").InnerText;
                var empresa = htmldocument.DocumentNode.SelectSingleNode("//span[contains(@class, 'result-item__company-label')]").InnerText;

                var link = htmldocument.DocumentNode.SelectSingleNode("//a[contains(@class, 'result-item__link')]").GetAttributeValue("href", "");

                newVaga.empresa = empresa;
                newVaga.titulo = titulo;
                newVaga.salario = detalhe;
                newVaga.url = link;

                _lista.Add(newVaga);
            }


            return _lista;

        }

        }
    }
