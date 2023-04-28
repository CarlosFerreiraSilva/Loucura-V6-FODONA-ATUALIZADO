using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace CoderCarrer.Models
{
    public class ConexaoBD
    {
        private static MySqlConnection Banco;
        public static MySqlConnection getConexao()
        {
                if (Banco == null)
                {
                    Banco = new MySqlConnection(@"Initial Catalog=projeto_crawler;User ID=root;Password=;");
                }
            //string connectionString = "Initial Catalog=nome_do_banco_de_dados;User ID=usuario_do_banco;Password=senha_do_banco;";
            return Banco;
            }
        //@"Server=127.0.0.1;Database=projeto_crawler;Uid=root;Pwd=cmfs110110;"
    }
}
