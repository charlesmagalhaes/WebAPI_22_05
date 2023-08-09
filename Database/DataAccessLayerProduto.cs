using Database.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
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

    public class DataAccessLayerProduto : IDataAccessLayer<ProdutoModel>
    {
        private DatabaseConnection _databaseConnection = new DatabaseConnection();

        public void Atualizar(ProdutoModel objeto)
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

                        if (objeto.Nome != null)
                        {
                            commandText += " nome = @Nome,";
                            parameters.Add(new Npgsql.NpgsqlParameter("@Nome", objeto.Nome));
                        }

                        if (objeto.Descricao != null)
                        {
                            commandText += " descricao = @Descricao,";
                            parameters.Add(new Npgsql.NpgsqlParameter("@Descricao", objeto.Descricao));
                        }

                        commandText = commandText.TrimEnd(',');

                        command.CommandText = $"{commandText} WHERE id = @Id;";
                        parameters.Add(new Npgsql.NpgsqlParameter("@Id", objeto.Id));
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

        public HttpResponseMessage Excluir(int id)
        {
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
                            // Produto excluído com sucesso
                            var response = new HttpResponseMessage(HttpStatusCode.OK);
                            response.Content = new StringContent("Exclusão feita com sucesso.", Encoding.UTF8, "text/plain");
                            return response;
                        }
                        else
                        {
                            // Produto não foi excluído
                            var response = new HttpResponseMessage(HttpStatusCode.Conflict);
                            response.Content = new StringContent("Não foi possível excluir o produto.", Encoding.UTF8, "text/plain");
                            return response;
                        }
                    }
                    catch (NpgsqlException e)
                    {
                        // Erro ao tentar excluir o produto
                        var response = new HttpResponseMessage(HttpStatusCode.Conflict);
                        response.Content = new StringContent("Erro ao tentar excluir: " + e.Message, Encoding.UTF8, "text/plain");
                        return response;
                    }
                }
            }
        }


        public List<ProdutoModel> BuscarLista()
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


        public ProdutoModel BuscarPorId(int id)
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

        public int Salvar(ProdutoModel objeto)
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

                        command.CommandText = $"INSERT INTO produto (nome, descricao) VALUES ('{objeto.Nome}', '{objeto.Descricao}') RETURNING id";
                        int idCriado = (int)command.ExecuteScalar();
                        return idCriado;
                    }
                    catch (Npgsql.PostgresException e)
                    {
                        // Trate a exceção de violação de campo único aqui
                        // Por exemplo, você pode exibir uma mensagem de erro para o usuário informando que o campo já existe
                        Console.WriteLine("Erro ao cadastrar produto: " + e.Message);
                        return 0;
                    }
                }
            }
        }
    }

      
    }
