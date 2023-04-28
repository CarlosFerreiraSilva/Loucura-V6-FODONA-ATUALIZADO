using AngleSharp.Html.Parser;
using CoderCarrer.Models;
using HtmlAgilityPack;
using System.Text;

namespace CoderCarrer.Domain
{
    public interface IExtratorVaga
    {
        public List<Vaga> getVagas();

    }
}
