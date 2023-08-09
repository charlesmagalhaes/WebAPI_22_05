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

    public class DataAccessLayerPreco : DatabaseConnection, IDataAccessLayer<PrecoModel>
    {
      

        public void Atualizar(PrecoModel objeto)
        {
            throw new NotImplementedException();
        }

        public List<PrecoModel> BuscarLista()
        {
            var precos = new List<PrecoModel>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT id, produto_id, preco, data FROM preco; ", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var preco = new PrecoModel
                            {
                                Id = reader.GetInt32(0),
                                ProdutoId = reader.GetInt32(1),
                                Preco = reader.GetInt32(2),
                                Data = reader.GetDateTime(3)
                            };

                            precos.Add(preco);
                        }
                    }
                }
            }

            return precos;
        }

        public PrecoModel BuscarPorId(int id)
        {
            PrecoModel preco = null;

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT id, produto_id, preco, data FROM preco WHERE produto_id = @id ; ", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            preco = new PrecoModel
                            {
                                Id = reader.GetInt32(0),
                                ProdutoId = reader.GetInt32(1),
                                Preco = reader.GetInt32(2),
                                Data = reader.GetDateTime(3)
                            };
                        }
                    }
                }
            }

            return preco;
        }

        public HttpResponseMessage Excluir(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new Npgsql.NpgsqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;

                        command.CommandText = $"DELETE FROM preco WHERE produto_id = {id}";
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
                            response.Content = new StringContent("Não foi possível excluir o preco.", Encoding.UTF8, "text/plain");
                            return response;
                        }
                    }
                    catch (Npgsql.PostgresException e)
                    {
                        var response = new HttpResponseMessage(HttpStatusCode.Conflict);
                        response.Content = new StringContent("Erro ao tentar excluir: " + e.Message, Encoding.UTF8, "text/plain");
                        return response;
                    }
                }
            };
        }

        public int Salvar(PrecoModel objeto)
        {
            throw new NotImplementedException();
        }
    }
}
