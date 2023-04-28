namespace CoderCarrer.Models
{
    public class Vaga
    {
            public int id_vaga { get; set; }
            public string titulo { get; set; }
            public string salario { get; set; }
            public string descricao_vaga { get; set; }
            public string empresa { get; set; }
            public string url { get; set; }
            public DateTime dataColeta { get; set; }
            public string EmpresaColeta { get; set; }


        

        public enum EmpresasColetas
        {
            TrabalhaBrasil = 1,
            VagaCom = 2,


        }
    }
}
