using AngleSharp.Html.Parser;
using CoderCarrer.Domain;
using CoderCarrer.Models;
using HtmlAgilityPack;

namespace CoderCarrer.DAL
{
         
        public class VagasTrovit : IExtratorVaga
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
                var content = await httpClient.GetStringAsync("https://empregos.trovit.com.br/index.php/cod.search_jobs/what_d.Programador/sug.0/isUserSearch.1");
                var document = await parser.ParseDocumentAsync(content);
                var doc = new HtmlDocument();
                doc.LoadHtml(document.DocumentElement.OuterHtml);


                var vaga = doc.DocumentNode.SelectNodes("//div[contains(@class, 'item-info')]");
                _lista = new List<Vaga>();
                foreach (var item in vaga)
                {

                    try
                    {
                        Vaga newVaga = new Vaga();
                        var htmldocument = new HtmlDocument();
                        htmldocument.LoadHtml(item.InnerHtml);

                    var titulo = htmldocument.DocumentNode.SelectSingleNode("//h4").InnerText;
                        var detalhe = htmldocument.DocumentNode.SelectSingleNode("//span[contains(@class, 'company-address')]").InnerText;
                        var empresa = htmldocument.DocumentNode.SelectSingleNode("//span[contains(@class, 'company')]").InnerText;
                        var descricao = htmldocument.DocumentNode.SelectSingleNode("//p[contains(@class, 'description')]").InnerText;
                        var link = htmldocument.DocumentNode.SelectSingleNode("//a[contains(@class, 'js-item-title')]").GetAttributeValue("href", "");

                        newVaga.titulo = titulo;
                        newVaga.empresa = empresa;
                        newVaga.descricao_vaga = descricao;
                        newVaga.salario = detalhe;
                        newVaga.url = link;

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

