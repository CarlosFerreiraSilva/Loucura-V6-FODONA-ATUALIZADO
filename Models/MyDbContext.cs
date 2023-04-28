using Microsoft.EntityFrameworkCore;

namespace CoderCarrer.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }


        public int id_vaga { get; set; }
        public string data_vaga { get; set; }
        public string salario { get; set; }
        public string descricao_vaga { get; set; }
        public string empresa { get; set; }
    }
}
