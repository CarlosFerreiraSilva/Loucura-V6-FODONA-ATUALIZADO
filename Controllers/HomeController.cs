using CoderCarrer.DAL;
using CoderCarrer.Domain;
using CoderCarrer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text.Json;
using System.Timers;

namespace CoderCarrer.Controllers
{


    public class HomeController : Controller
    {
        private int _take = 30;
        private VagaDAO dados = new VagaDAO();
        private readonly ILogger<HomeController> _logger;
        private static System.Timers.Timer aTimer;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        int x = 30;
        public async Task<IActionResult> Index(string vagas, int skip = 0)
        {
            //SetTimer();
            var vagas_ = await dados.getVagasDB();
            ViewBag.limit = vagas_.Count - x;
           

            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private static void SetTimer()
        {
            // Create a timer with a 24 hour interval.
            aTimer = new System.Timers.Timer(10000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        [HttpGet]
        private static void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            VagaDAO vagaInsert = new VagaDAO();

            vagaInsert.sincronizarInserindoVagas(new TrabalhaBrasilExtrator());
            vagaInsert.sincronizarInserindoVagas(new VagasComExtrator());
            //vagaInsert.sincronizarInserindoVagas(new VagasTrovitExtrator());

        }

     

        public IActionResult Privacy()
        {
            return View();
        }
        
        public async Task<IActionResult> CarregarMais(int skip = 1, string pesquisa = "")
        {
            var vagas_ = await dados.getVagasDB();
            return PartialView("_vagas", vagas_!.Where(x => 
                x.titulo.Contains(pesquisa ?? "", StringComparison.InvariantCultureIgnoreCase)
                | x.empresa.Contains(pesquisa ?? "", StringComparison.InvariantCultureIgnoreCase)).Take((skip + 1) * _take).ToList());
        }
    }
}