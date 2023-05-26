using Database.Models;
using System;

namespace Negocios.Models
{
    public class PrecoResponse
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public decimal Preco { get; set; }
        public DateTime Data { get; set; }

        public static explicit operator PrecoResponse(PrecoModel source)
        {
            return new PrecoResponse()
            {
                Id = source.Id,
                ProdutoId = source.ProdutoId,
                Preco = source.Preco,
                Data = source.Data
            };
        }
    }

}