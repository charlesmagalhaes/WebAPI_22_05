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
    public interface IDataAccessLayerPreco
    {
        PrecoModel GetPrecotById(int id);
        void SalvarPreco(PrecoModel preco); 
        List<PrecoModel> GetListPreco();
        void ExcluirPreco(int id);
        void AtualizarPreco(PrecoModel preco);
    }

    public class DataAccessLayerPreco : IDataAccessLayerPreco
    {
        private readonly DatabaseConnection _databaseConnection = new DatabaseConnection();

        public DataAccessLayerPreco()
        {
          
        }

        public void AtualizarPreco(PrecoModel preco)
        {
            throw new NotImplementedException();
        }

        public void ExcluirPreco(int id)
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

                        command.CommandText = $"DELETE FROM preco WHERE produto_id = {id}";
                        command.ExecuteNonQuery();
                    }
                    catch (Npgsql.PostgresException e)
                    {
                        // Trate a exceção de violação de campo único aqui
                        // Por exemplo, você pode exibir uma mensagem de erro para o usuário informando que o campo já existe
                        Console.WriteLine("Erro excluir preco: " + e.Message);
                    }
                }
            };
        }

        public List<PrecoModel> GetListPreco()
        {
            var precos = new List<PrecoModel>();

            using (var connection = _databaseConnection.GetConnection())
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

        public PrecoModel GetPrecotById(int id)
        {
            PrecoModel preco = null;

            using (var connection = _databaseConnection.GetConnection())
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

        public void SalvarPreco(PrecoModel preco)
        {
            throw new NotImplementedException();
        }
    }

}
