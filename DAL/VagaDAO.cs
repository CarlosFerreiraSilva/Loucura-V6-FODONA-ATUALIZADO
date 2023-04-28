using CoderCarrer.Domain;
using CoderCarrer.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace CoderCarrer.DAL
{
    public class VagaDAO
    {
        MySqlConnection conexao;
        public VagaDAO()
        {
            conexao = ConexaoBD.GetConexao();

        }

        public bool sincronizarInserindoVagas(IExtratorVaga pvagas)
        {


            List<Vaga> lista = pvagas.getVagas();
            foreach (var item in lista)
            {
                string Mysql = "insert into vaga ( descricao_vaga, empresa, salario, titulo, url, dataColeta, EmpresaColeta) values ( @descricao_vaga, @empresa, @salario, @titulo, @url, now(), @EmpresaColeta);";
                int qtdInserida = conexao.Execute(Mysql, item);

            }
            return false;
        }


        public List<Vaga> getData()
        {
            string sql = "SELECT * FROM vaga;";
            var dados = (List<Vaga>)conexao.Query<Vaga>(sql);
            return dados;
        }


        public async Task<List<Vaga>> getVagasDB()

        {

            var queryEmpresa = "select distinct EmpresaColeta from vaga ";
            List<Vaga> empresas = (List<Vaga>)await conexao.QueryAsync<Vaga>(queryEmpresa);

            List<Vaga> todos = new List<Vaga>();
            foreach (var item in empresas)
            {
                var select = "select * from vaga where EmpresaColeta ='" + item.EmpresaColeta + "'";
                List<Vaga> vagasempresa = (List<Vaga>)conexao.Query<Vaga>(select);
                var ultimaVagaColetada = vagasempresa.OrderBy(x => x.id_vaga).LastOrDefault();
                if (ultimaVagaColetada != null)
                {
                    var selectEmpresaUltima = "select * from vaga where EmpresaColeta ='" + item.EmpresaColeta + "' and dataColeta= DATE('" + ultimaVagaColetada.dataColeta.ToString("yyyy-MM-dd") + "')";

                    List<Vaga> listaVagaEmpresaAtual = (List<Vaga>)conexao.Query<Vaga>(selectEmpresaUltima);

                    todos.AddRange(listaVagaEmpresaAtual);
                }

            }


            return todos;
        }



    }
}