using Database.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public interface IDataAccessLayer
    {
        ProdutoModel GetProductById(int id);
        void SalvarProduto(ProdutoModel product); 
        List<ProdutoModel> GetListProduct();
    }

    public class DataAccessLayer : IDataAccessLayer
    {
        private DatabaseConnection _databaseConnection = new DatabaseConnection();

        public DataAccessLayer()
        {
        }

        public DataAccessLayer(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public List<ProdutoModel> GetListProduct()
        {
            var produto = new List<ProdutoModel>();

            using (var connection = _databaseConnection.GetConnection())
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT nome, descricao FROM produto", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var produtoNovo = new ProdutoModel
                            {
                                Nome = reader.GetString(0),
                                Descricao = reader.GetString(1)
                             

                            };
                            produto.Add(produtoNovo);
                        }
                    }
                }
            }

            return produto;
        }

        public ProdutoModel GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public void SalvarProduto(ProdutoModel produto)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                connection.Open();
                using (var command = new Npgsql.NpgsqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;

                        command.CommandText = $"INSERT INTO produto (nome, descricao) VALUES ('{produto.Nome}', '{produto.Descricao}')";
                        command.ExecuteNonQuery();
                    }
                    catch (Npgsql.PostgresException e)
                    {
                        // Trate a exceção de violação de campo único aqui
                        // Por exemplo, você pode exibir uma mensagem de erro para o usuário informando que o campo já existe
                        Console.WriteLine("Erro ao cadastrar produto: " + e.Message);
                    }
                }
            }
        }
    }


}
