using MySql.Data.MySqlClient;

namespace CoderCarrer.DAL
{
        public class ConexaoBD
        {
            private static MySqlConnection conexao;
            public static MySqlConnection GetConexao()
            {
                if (conexao == null)
                {
                    conexao = new MySqlConnection(@"server=localhost;user=root;password=123456;database=projeto_crawler;");
                    //"server=localhost;user=root;password=password;database=mydatabase"
                }

                return conexao;
            }

        }
    
}
