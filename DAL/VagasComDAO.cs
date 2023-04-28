using AngleSharp.Html.Parser;
using CoderCarrer.Domain;
using CoderCarrer.Models;
using HtmlAgilityPack;


namespace CoderCarrer.DAL
{
    public class VagaComDAO : IExtratorVaga
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
            var content = await httpClient.GetStringAsync("https://www.vagas.com.br/vagas-de-programador");
            var document = await parser.ParseDocumentAsync(content);
            var doc = new HtmlDocument();
            doc.LoadHtml(document.DocumentElement.OuterHtml);


            var vaga = doc.DocumentNode.SelectNodes("//li[contains(@class, 'vaga odd ')]");
            _lista = new List<Vaga>();
            foreach (var item in vaga)
            {

                try
                {
                    Vaga newVaga = new Vaga();
                    var htmldocument = new HtmlDocument();
                    htmldocument.LoadHtml(item.InnerHtml);

                    var titulo = htmldocument.DocumentNode.SelectSingleNode("//a[contains(@class, 'link-detalhes-vaga')]").InnerText;
                    var detalhe = htmldocument.DocumentNode.SelectSingleNode("//div[contains(@class, 'nivelQtdVagas')]").InnerText;
                    var empresa = htmldocument.DocumentNode.SelectSingleNode("//span[contains(@class, 'emprVaga')]").InnerText;
                    var descricao = htmldocument.DocumentNode.SelectSingleNode("//div[contains(@class, 'detalhes')]").InnerText;
                    var link = htmldocument.DocumentNode.SelectSingleNode("//a[contains(@class, 'link-detalhes-vaga')]").GetAttributeValue("href", "");

                    newVaga.titulo = titulo;
                    newVaga.empresa = empresa;
                    newVaga.descricao_vaga = descricao;
                    newVaga.salario = detalhe;
                    newVaga.url = "https://www.vagas.com.br/" + link;

                    newVaga.titulo = titulo;

                    _lista.Add(newVaga);
                }
                catch (Exception)
                {

                    continue;
                }

            }


            return _lista;


        }


    }
    }






