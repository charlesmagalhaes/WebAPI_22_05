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


    public class ExclusaoProdutoResponse
    {
        public bool Sucesso { get; set; }
        public bool StatusRequisicao { get; set; }
        public string Mensagem { get; set; }
        public string MensagemErro { get; set; }
    }
    public interface IDataAccessLayer
    {
        ProdutoModel GetProductById(int id);
        void SalvarProduto(ProdutoModel product); 
        List<ProdutoModel> GetListProduct();
        ExclusaoProdutoResponse ExcluirProduto(int id);
        void AtualizarProduto(ProdutoModel product);
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

        public void AtualizarProduto(ProdutoModel product)
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

                        var commandText = "UPDATE produto SET";
                        var parameters = new List<Npgsql.NpgsqlParameter>();

                        if (product.Nome != null)
                        {
                            commandText += " nome = @Nome,";
                            parameters.Add(new Npgsql.NpgsqlParameter("@Nome", product.Nome));
                        }

                        if (product.Descricao != null)
                        {
                            commandText += " descricao = @Descricao,";
                            parameters.Add(new Npgsql.NpgsqlParameter("@Descricao", product.Descricao));
                        }

                        commandText = commandText.TrimEnd(',');

                        command.CommandText = $"{commandText} WHERE id = @Id;";
                        parameters.Add(new Npgsql.NpgsqlParameter("@Id", product.Id));
                        command.Parameters.AddRange(parameters.ToArray());

                        command.ExecuteNonQuery();
                    }
                    catch (Npgsql.PostgresException e)
                    {
                        // Trate a exceção de violação de campo único aqui
                        // Por exemplo, você pode exibir uma mensagem de erro para o usuário informando que o campo já existe
                        Console.WriteLine("Erro ao atualizar produto: " + e.Message);
                    }
                }
            }
        }


        public ExclusaoProdutoResponse ExcluirProduto(int id)
        {
            var response = new ExclusaoProdutoResponse();

            using (var connection = _databaseConnection.GetConnection())
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = "SELECT excluir_produto(@id)";
                        command.Parameters.AddWithValue("id", id);

                        var result = command.ExecuteScalar();

                        if (result != null)
                        {
                            response.Sucesso = true;
                            response.StatusRequisicao = true;
                            response.Mensagem = "Exclusão feita com sucesso.";
                        }
                        else
                        {
                            response.Sucesso = false;
                            response.StatusRequisicao = false;
                            response.Mensagem = "Erro ao chamar a função de exclusão do produto.";
                        }
                    }
                    catch (NpgsqlException e)
                    {
                        response.Sucesso = false;
                        response.StatusRequisicao = false;
                        response.MensagemErro = "Erro ao chamar a função de exclusão do produto: " + e.Message;
                    }
                }
            }

            return response;
        }





        public List<ProdutoModel> GetListProduct()
        {
            var produtos = new List<ProdutoModel>();

            using (var connection = _databaseConnection.GetConnection())
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT id, nome, descricao FROM produto ORDER BY nome", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var produto = new ProdutoModel
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Descricao = reader.GetString(2)
                            };

                            produtos.Add(produto);
                        }
                    }
                }
            }

            return produtos;
        }


        public ProdutoModel GetProductById(int id)
        {
            ProdutoModel produto = null;

            using (var connection = _databaseConnection.GetConnection())
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT id, nome, descricao FROM produto WHERE id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            produto = new ProdutoModel
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Descricao = reader.GetString(2)
                            };
                        }
                    }
                }
            }

            return produto;
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
